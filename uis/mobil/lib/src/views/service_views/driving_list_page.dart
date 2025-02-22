import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobil/src/models/update_student_status_model.dart';
import 'package:mobil/src/views/service_views/service_dashboard.dart';

import '../../models/student_service_model.dart';
import '../../service/service_service.dart';
import '../../widgets/google_map_page.dart';

class DrivingListPage extends StatefulWidget {
  final int tripType;

  const DrivingListPage({
    super.key,
    required this.tripType
  });

  @override
  State<DrivingListPage> createState() => _DrivingListPageState();
}

class _DrivingListPageState extends State<DrivingListPage> {
  final ServiceService _serviceService = ServiceService();

  List<StudentServiceModel> students = [];
  late ScrollController _scrollController;

  Future<void> _fetchDrivingList() async {
    final fetchedStudents = await _serviceService.getStudents();
    if (fetchedStudents != null) {
      setState(() {
        students = fetchedStudents.map((e) => StudentServiceModel.fromJson(e)).toList();
        students.sort((a, b) => (a.sortIndex as int).compareTo(b.sortIndex as int));

        if (widget.tripType == 1) {
          students = students.reversed.toList();
        }
      });
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

  Future<void> _updateAllStudentsStatus() async {
    for (var student in students) {
      if (_isAttending(student)) {
        await _serviceService.updateStatus(
          student.studentId!,
          UpdateStudentStatusModel(
            status: "GetOff",
            direction: widget.tripType == 0 ? "ToSchool" : "FromSchool",
          ),
        );
        setState(() {
          student.status = "GetOff";
        });
      }
    }
  }

  void _handleNavigation(int index) async {
    if (index == 0) {
      // ServiceDashboard'a gitme işlemi
      bool confirm = await _showConfirmationDialog(
          'Çıkış Onayı',
          'Sürüş bitiriliyor. Çıkmak istediğinize emin misiniz?'
      );

      if (confirm && mounted) {
        await _updateAllStudentsStatus();

        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (context) => const ServiceDashboard()),
        );
      }
    } else if (index == 2) {
      // GoogleMapPage'e gitme işlemi
      bool confirm = await _showConfirmationDialog(
          'Harita Görünümü',
          'Sürüşü harita kullanarak yapmak istediğinize emin misiniz? Eğer ki geçiş yaparsanız yoklamaya baştan başlamanız gerekecek.'
      );

      if (confirm && mounted) {
        await _updateAllStudentsStatus();

        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (context) => GoogleMapPage(isReturnTrip: widget.tripType)),
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
    _scrollController = ScrollController();
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  Future<void> _handleUpdateStudentStatus(int? studentId, String status) async {
    UpdateStudentStatusModel updatedStudent = UpdateStudentStatusModel (
        status: status,
        direction: _getTripType()
    );

    final response = await _serviceService.updateStatus(studentId!, updatedStudent);
    if (response) {
      print(response);
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Öğrenci durumu başarıyla güncellendi.")),
      );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Durum güncellenemedi.")),
      );
    }
  }

  String _getTripType() {
    if (widget.tripType == 0) {
      return "ToSchool";
    } else {
      return "FromSchool";
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

  int _getTotalAttending() {
    return students.where(_isAttending).length;
  }

  void _scrollToStudent(int index) {
    final double offset = index * 130.0;
    if (offset > _scrollController.position.maxScrollExtent) {
      return;
    }

    _scrollController.animateTo(
      offset,
      duration: const Duration(milliseconds: 500),
      curve: Curves.easeInOut,
    );
  }

  DateTime onlyDate(DateTime date) {
    return DateTime(date.year, date.month, date.day);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: false,
        title: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            const Text('Yoklama Listesi'),
            if (students.isNotEmpty)
              Text(
                '${_getTotalAttending()}/${students.length}',
                style: const TextStyle(fontSize: 16),
              ),
          ],
        ),
      ),
      body: ListView.builder(
        controller: _scrollController,
        itemCount: students.length,
        itemBuilder: (context, index) {
          final student = students[index];

          return Card(
            margin: const EdgeInsets.symmetric(vertical: 8, horizontal: 16),
            child: Padding(
              padding: const EdgeInsets.all(12.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Row(
                            children: [
                              Text(
                                student.name,
                                style: TextStyle(
                                  fontSize: 18,
                                  fontWeight: FontWeight.bold,
                                  color: _isAttending(student) ? Colors.black : Colors.grey,
                                ),
                              ),
                              if (_isAttending(student))
                                const SizedBox(width: 6),
                              if (!_isAttending(student))
                                const Icon(
                                  Icons.circle,
                                  size: 10,
                                  color: Colors.red,
                                ),
                            ],
                          ),
                        ],
                      ),
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            student.parentName,
                            style: TextStyle(fontSize: 16, color: !_isAttending(student) ? Colors.grey : Colors.black),
                          ),
                          Text(
                            student.parentPhone,
                            style: TextStyle(color: !_isAttending(student) ? Colors.grey : Colors.black),
                          ),
                        ],
                      ),
                      Switch(
                        value: student.status == "GetOn",
                        onChanged: !_isAttending(student)
                            ? null
                            : (value) async {
                          setState(() {
                            student.status = value ? "GetOn" : "GetOff";
                          });
                          await _handleUpdateStudentStatus(student.studentId, student.status);
                          if (value) {
                            _scrollToStudent(index);
                          }
                        },
                      ),
                    ],
                  ),
                  Padding(
                    padding: const EdgeInsets.only(top: 8.0),
                    child: Text(
                      'Not: ${student.driverNote}',
                      style: TextStyle(
                        fontStyle: FontStyle.italic,
                        color: !_isAttending(student) ? Colors.grey : Colors.grey.shade700,
                      ),
                    ),
                  ),
                ],
              ),
            ),
          );
        },
      ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: 1,
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