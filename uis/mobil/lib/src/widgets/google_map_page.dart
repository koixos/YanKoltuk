import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:location/location.dart';
import 'package:flutter_polyline_points/flutter_polyline_points.dart';
import 'package:mobil/src/models/student_service_model.dart';
import 'package:mobil/src/views/service_views/driving_list_page.dart';
import 'package:mobil/src/views/service_views/service_dashboard.dart';

import '../api/api_key.dart';
import '../models/update_student_status_model.dart';
import '../service/service_service.dart';


class GoogleMapPage extends StatefulWidget {
  final int isReturnTrip;
  const GoogleMapPage({super.key, required this.isReturnTrip});

  @override
  State<GoogleMapPage> createState() => _GoogleMapPageState();
}

class _GoogleMapPageState extends State<GoogleMapPage> {
  final locationController = Location();
  final ServiceService _serviceService = ServiceService();

  List<StudentServiceModel> students = [];
  LatLng? currentPosition;
  GoogleMapController? _mapController;
  final Set<Marker> _markers = {};
  final List<LatLng> _polylineCoordinates = [];
  final PolylinePoints polylinePoints = PolylinePoints();
  String? selectedMarkerId;

  Future<void> _fetchDrivingList() async {
    final fetchedStudents = await _serviceService.getStudents();
    if (fetchedStudents != null) {
      setState(() {
        students = fetchedStudents.map((e) => StudentServiceModel.fromJson(e)).toList();
        students.sort((a, b) => (a.sortIndex as int).compareTo(b.sortIndex as int));

        if (widget.isReturnTrip == 1) {
          students = students.reversed.toList();
        }
      });
      await _loadMarkersFromJson();
    }
  }

  bool _isAttending(StudentServiceModel student) {
    DateTime today = DateTime.now();

    if (student.excludedStartDate == null && student.excludedEndDate == null) {
      return true;
    } else if (onlyDate(student.excludedStartDate!) == onlyDate(today) || onlyDate(student.excludedEndDate!) == onlyDate(today)) {
      return false;
    }
    return !(student.excludedStartDate!.isBefore(today) && student.excludedEndDate!.isAfter(today));
  }

  DateTime onlyDate(DateTime date) {
    return DateTime(date.year, date.month, date.day);
  }

  Future<void> _updateAllStudentsStatus() async {
    for (var student in students) {
      if (_isAttending(student)) {
        await _serviceService.updateStatus(
          student.studentId!,
          UpdateStudentStatusModel(
            status: "GetOff",
            direction: widget.isReturnTrip == 0 ? "ToSchool" : "FromSchool",
          ),
        );
        setState(() {
          student.status = "GetOff";
        });
      }
    }
  }

  Future<bool> _showConfirmationDialog(String title, String message) async {
    return await showDialog<bool>(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(title),
          content: Text(message),
          actions: <Widget>[
            TextButton(
              child: const Text('Hayır'),
              onPressed: () {
                Navigator.of(context).pop(false);
              },
            ),
            TextButton(
              child: const Text('Evet'),
              onPressed: () {
                Navigator.of(context).pop(true);
              },
            ),
          ],
        );
      },
    ) ?? false;
  }

  void _handleNavigation(int index) async {
    if (index == 0) {
      // ServiceDashboard'a gitme işlemi
      bool confirm = await _showConfirmationDialog(
          'Çıkış Onayı',
          'Sürüş bitiriliyor ve tüm öğrenciler indi olarak işaretleniyor. Çıkmak istediğinize emin misiniz?'
      );

      if (confirm && mounted) {
        await _updateAllStudentsStatus();

        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (context) => const ServiceDashboard()),
        );
      }
    } else if (index == 1) {
      bool confirm = await _showConfirmationDialog(
          'Liste Görünümü',
          'Sürüşü liste kullanarak yapmak istediğinize emin misiniz? Eğer ki geçiş yaparsanız yoklamaya baştan başlamanız gerekecek.'
      );

      if (confirm && mounted) {
        await _updateAllStudentsStatus();

        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (context) => DrivingListPage(tripType: widget.isReturnTrip)),
        );
      }
    }
  }

  @override
  void initState() {
    super.initState();
    _fetchDrivingList().then((_) {
      // Öğrenci listesi yüklendikten sonra statusları güncelle
      _updateAllStudentsStatus();
    });
    WidgetsBinding.instance.addPostFrameCallback((_) async => await fetchLocationUpdates());
    _loadMarkersFromJson();
  }

  Future<void> _loadMarkersFromJson() async {
    try {
      setState(() {
        for (var student in students) {
          if(_isAttending(student)){
            final LatLng position = LatLng(student.latitude!, student.longitude!);
            _markers.add(
              Marker(
                markerId: MarkerId(student.schoolNo),
                icon: BitmapDescriptor.defaultMarkerWithHue(BitmapDescriptor.hueRed),
                position: position,
                infoWindow: InfoWindow(title: student.name, snippet: student.driverNote),
                onTap: () {
                  _showStudentDetails(student);
                },
              ),
            );
          }
        }
      });
    } catch (e) {
      debugPrint('Error loading markers from JSON: $e');
    }
  }

  void _showStudentDetails(StudentServiceModel student) {
    showModalBottomSheet(
      context: context,
      builder: (context) {
        return Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                student.name,
                style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 8),
              Text(
                'Not: ${student.driverNote}',
                style: const TextStyle(fontStyle: FontStyle.italic, color: Colors.grey),
              ),
              const SizedBox(height: 8),
              SwitchListTile(
                title: const Text('Yoklama Durumu'),
                value: student.status == "GetOn",
                onChanged: (value) async {
                  final newStatus = value ? "GetOn" : "GetOff";
                  await _handleUpdateStudentStatus(student.studentId, newStatus);
                  setState(() {
                    student.status = newStatus;
                  });
                  Navigator.pop(context); // BottomSheet'i kapat
                },
              ),
            ],
          ),
        );
      },
    );
  }

  Future<void> _handleUpdateStudentStatus(int? studentId, String status) async {
    final response = await _serviceService.updateStatus(
      studentId!,
      UpdateStudentStatusModel(
        status: status,
        direction: widget.isReturnTrip == 0 ? "ToSchool" : "FromSchool",
      ),
    );
    if (response) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Öğrenci durumu başarıyla güncellendi.")),
      );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Durum güncellenemedi.")),
      );
    }
  }

  Future<void> fetchLocationUpdates() async {
    bool serviceEnabled;
    PermissionStatus permissionGranted;

    serviceEnabled = await locationController.serviceEnabled();
    if (!serviceEnabled) {
      serviceEnabled = await locationController.requestService();
      if (!serviceEnabled) {
        return;
      }
    }

    permissionGranted = await locationController.hasPermission();
    if (permissionGranted == PermissionStatus.denied) {
      permissionGranted = await locationController.requestPermission();
      if (permissionGranted != PermissionStatus.granted) {
        return;
      }
    }

    locationController.onLocationChanged.listen((currentLocation) {
      if (currentLocation.latitude != null && currentLocation.longitude != null) {
        setState(() {
          currentPosition = LatLng(
            currentLocation.latitude!,
            currentLocation.longitude!,
          );

          _markers.removeWhere((marker) => marker.markerId == const MarkerId('currentLocation'));
          _markers.add(
            Marker(
              markerId: const MarkerId('currentLocation'),
              icon: BitmapDescriptor.defaultMarkerWithHue(BitmapDescriptor.hueBlue),
              position: currentPosition!,
              infoWindow: const InfoWindow(title: 'Current Location'),
            ),
          );

          _mapController?.animateCamera(
            CameraUpdate.newLatLngZoom(currentPosition!, 16),
          );
        });
      }
    });
  }

  Future<void> _calculateRoutesSequentially() async {
    if (currentPosition == null) return;

    List<PointLatLng> waypoints = [];
    List<LatLng> routePoints = [];

    // Mevcut konumu başlangıç noktası olarak ekle
    routePoints.add(currentPosition!);

    for (var student in students) {
      if(_isAttending(student)){
        routePoints.add(LatLng(student.latitude!, student.longitude!));
        waypoints.add(PointLatLng(student.latitude!, student.longitude!));
      }
    }

    _polylineCoordinates.clear();
    for (int i = 0; i < routePoints.length - 1; i++) {
      PolylineResult result = await polylinePoints.getRouteBetweenCoordinates(
        apiKey, // Buraya kendi Google Maps API Key'inizi ekleyin
        PointLatLng(routePoints[i].latitude, routePoints[i].longitude),
        PointLatLng(routePoints[i + 1].latitude, routePoints[i + 1].longitude),
      );

      if (result.points.isNotEmpty) {
        for (var point in result.points) {
          _polylineCoordinates.add(LatLng(point.latitude, point.longitude));
        }
      }
    }

    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: false,
        title: const Text('Şoförün Konumu'),
        actions: [
          IconButton(
            icon: const Icon(Icons.route),
            onPressed: _calculateRoutesSequentially,
          ),
        ],
      ),
      body: Column(
        children: [
          Expanded(
            child: Container(
              margin: const EdgeInsets.all(8.0),
              decoration: BoxDecoration(
                border: Border.all(color: Colors.black, width: 2.0),
                borderRadius: BorderRadius.circular(12.0),
              ),
              child: GoogleMap(
                initialCameraPosition: const CameraPosition(
                  target: LatLng(39.92077, 32.85411),
                  zoom: 2,
                ),
                markers: _markers,
                myLocationEnabled: true,
                myLocationButtonEnabled: true,
                polylines: {
                  Polyline(
                    polylineId: const PolylineId('route'),
                    color: Colors.blue,
                    width: 5,
                    points: _polylineCoordinates,
                  )
                },
                onMapCreated: (GoogleMapController controller) {
                  _mapController = controller;
                },
              ),
            ),
          ),
        ],
      ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: 2, // Bu ekran için mevcut index
        onTap: _handleNavigation,
        type: BottomNavigationBarType.fixed,
        selectedItemColor: Colors.blueAccent,
        unselectedItemColor: Colors.grey,
        items: const [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Ana Sayfa'),
          BottomNavigationBarItem(icon: Icon(Icons.list), label: 'Servis Listesi'),
          BottomNavigationBarItem(icon: Icon(Icons.map_outlined), label: 'Harita'),
        ],
      ),
    );
  }
}
