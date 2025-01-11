import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:location/location.dart';

class LocationPickerPage extends StatefulWidget {
  @override
  _LocationPickerPageState createState() => _LocationPickerPageState();
}

class _LocationPickerPageState extends State<LocationPickerPage> {
  LatLng? selectedLocation; // Kullanıcının seçtiği konum
  LatLng? currentPosition; // Kullanıcının güncel konumu
  GoogleMapController? _mapController;
  final Location _locationController = Location();

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _fetchCurrentLocation(); // Sayfa açıldığında mevcut konumu al
    });
  }

  Future<void> _fetchCurrentLocation() async {
    bool serviceEnabled;
    PermissionStatus permissionGranted;

    // Konum servisinin aktif olup olmadığını kontrol et
    serviceEnabled = await _locationController.serviceEnabled();
    if (!serviceEnabled) {
      serviceEnabled = await _locationController.requestService();
      if (!serviceEnabled) {
        return;
      }
    }

    // Konum izni kontrolü
    permissionGranted = await _locationController.hasPermission();
    if (permissionGranted == PermissionStatus.denied) {
      permissionGranted = await _locationController.requestPermission();
      if (permissionGranted != PermissionStatus.granted) {
        return;
      }
    }

    // Mevcut konumu al
    LocationData locationData = await _locationController.getLocation();
    LatLng userCurrentPosition =
    LatLng(locationData.latitude ?? 0.0, locationData.longitude ?? 0.0);

    setState(() {
      currentPosition = userCurrentPosition;
      selectedLocation = userCurrentPosition; // Varsayılan olarak güncel konum seçili olsun
    });

    // Haritayı güncel konuma taşı
    _mapController?.animateCamera(
      CameraUpdate.newLatLngZoom(userCurrentPosition, 15),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Konum Seç"),
      ),
      body: GoogleMap(
        initialCameraPosition: CameraPosition(
          target: currentPosition ?? LatLng(39.92077, 32.85411), // Eğer mevcut konum alınamazsa Ankara'ya odaklan
          zoom: 12,
        ),
        onMapCreated: (GoogleMapController controller) {
          _mapController = controller;

          if (currentPosition != null) {
            // Haritayı mevcut konuma odakla
            _mapController?.animateCamera(
              CameraUpdate.newLatLngZoom(currentPosition!, 15),
            );
          }
        },
        myLocationEnabled: true,
        myLocationButtonEnabled: true,
        onTap: (LatLng location) {
          setState(() {
            selectedLocation = location;
          });
        },
        markers: selectedLocation != null
            ? {
          Marker(
            markerId: MarkerId("selected-location"),
            position: selectedLocation!,
            icon: BitmapDescriptor.defaultMarkerWithHue(BitmapDescriptor.hueRed),
          ),
        }
            : {},
      ),
      bottomNavigationBar: selectedLocation != null
          ? Padding(
        padding: const EdgeInsets.all(16.0),
        child: ElevatedButton(
          onPressed: () {
            Navigator.pop(context, selectedLocation);
          },
          child: Text("Bu Konumu Onayla"),
        ),
      )
          : null,
    );
  }
}
