import 'package:flutter/material.dart';
import 'package:mobil/src/models/student_service_model.dart';
import 'package:mobil/src/models/update_driver_note_model.dart';
import 'package:mobil/src/models/update_student_order_model.dart';
import 'package:mobil/src/views/service_dashboard.dart';

import '../service/service_service.dart';

class ServiceStudentList extends StatefulWidget {
  final int capacity;

  const ServiceStudentList({
    super.key,
    required this.capacity
  });

  @override
  State<ServiceStudentList> createState() => _ServiceStudentListState();
}

class _ServiceStudentListState extends State<ServiceStudentList> {
  final ServiceService _serviceService = ServiceService();

  List<StudentServiceModel> students = [];
  int capacity = 0;
  bool isEditMode = false;

  @override
  void initState() {
    super.initState();
    _fetchStudents();
    capacity = widget.capacity;
  }

  Future<void> _fetchStudents() async {
    final fetchedStudents = await _serviceService.getStudents();
    if (fetchedStudents != null) {
      setState(() {
        students = fetchedStudents
            .map((e) => StudentServiceModel.fromJson(e))
            .toList();
        students.sort((a, b) => (a.sortIndex as int).compareTo(b.sortIndex as int));
      });
    }
  }

  Future<void> _handleEditNote(int? studentId, String note) async {
    final updatedStudent = UpdateDriverNoteModel (
      driverNote: note
    );
    final response = await _serviceService.editNote(updatedStudent, studentId);
    if (response) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Not başarıyla güncellendi.")),
      );

      Future.delayed(const Duration(seconds: 1), () {
        Navigator.pushNamed(context, "/serviceStudentList");
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Not düzenlenemedi. Lütfen maksimum 50 karakter giriniz.")),
      );
    }
  }

  Future<void> _handleUpdateOrder() async {
    List<UpdateStudentOrderModel> updatedList = students.map((student) {
      return UpdateStudentOrderModel (
        studentId: student.studentId,
        sortIndex: students.indexOf(student),
      );
    }).toList();
    final response = await _serviceService.updateOrder(updatedList);
    if (response) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Öğrenci sıralaması başarıyla güncellendi.")),
      );

      Future.delayed(const Duration(seconds: 1), () {
        Navigator.pushNamed(context, "/serviceStudentList");
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Sıralama düzenlenemedi."
            "")),
      );
    }
  }

  void _showStudentDetailsDialog(StudentServiceModel student) {
    showDialog(
      context: context,
      builder: (context) {
        return Dialog(
          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(15)),
          child: Container(
            width: MediaQuery.of(context).size.width * 0.9,
            padding: const EdgeInsets.all(16.0),
            child: SingleChildScrollView(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Center(
                    child: Text(
                      student.name,
                      style: const TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                    ),
                  ),
                  const SizedBox(height: 16),
                  ..._buildInfoCards(student),
                  const SizedBox(height: 16),
                  Align(
                    alignment: Alignment.centerRight,
                    child: TextButton(
                      onPressed: () => Navigator.of(context).pop(),
                      child: const Text('Kapat', style: TextStyle(fontSize: 16)),
                    ),
                  ),
                ],
              ),
            ),
          ),
        );
      },
    );
  }

  void _showLoadingAndRefreshDialog(StudentServiceModel student) {
    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext dialogContext) {
        Future.delayed(const Duration(milliseconds: 500), () {
          Navigator.of(dialogContext).pop();
          Navigator.of(dialogContext).pop();
          _showStudentDetailsDialog(student);
        });
        return const Center(
          child: CircularProgressIndicator(),
        );
      },
    );
  }

  void _showEditNoteDialog(StudentServiceModel student) {
    final TextEditingController noteController = TextEditingController(text: student.driverNote ?? '');

    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: const Text('Not Düzenle'),
          content: SizedBox(
            width: 300,
            child: TextField(
              controller: noteController,
              maxLines: 3,
              decoration: const InputDecoration(
                hintText: 'Yeni not giriniz...',
                border: OutlineInputBorder(),
              ),
            ),
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.of(context).pop(),
              child: const Text('İptal'),
            ),
            TextButton(
              onPressed: () {
                _handleEditNote(student.studentId, noteController.text);

                Navigator.of(context).pop();
                _showLoadingAndRefreshDialog(student);
              },
              child: const Text('Kaydet'),
            ),
          ],
        );
      },
    );
  }

  List<Widget> _buildInfoCards(StudentServiceModel student) {
    final List<Map<String, String>> infoList = [
      {'title': 'Okul Numarası', 'value': student.schoolNo},
      {'title': 'Veli Adı', 'value': student.parentName},
      {'title': 'Veli Telefonu', 'value': student.parentPhone},
      {'title': 'Adres', 'value': student.address},
      {
        'title': 'Not',
        'value': student.driverNote == null || student.driverNote!.isEmpty ? '-' : student.driverNote!,
      },
    ];

    return infoList.map((info) {
      final bool isNote = info['title'] == 'Not';
      return Card(
        elevation: 2,
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
        margin: const EdgeInsets.symmetric(vertical: 8),
        child: Container(
          width: double.infinity,
          padding: const EdgeInsets.all(12.0),
          child: Row(
            children: [
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      info['title']!,
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.bold,
                        color: Colors.blueGrey,
                      ),
                    ),
                    const SizedBox(height: 8),
                    Text(
                      info['value']!,
                      style: const TextStyle(fontSize: 18),
                      maxLines: 2,
                      overflow: TextOverflow.ellipsis,
                    ),
                  ],
                ),
              ),
            if (isNote)
              IconButton(
                icon: const Icon(Icons.edit, color: Colors.blue),
                onPressed: () => _showEditNoteDialog(student),
            )],
          ),
        ),
      );
    }).toList();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: false,
        title: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            const Text('Öğrenci Listesi\n'),
            if (students.isNotEmpty && capacity > 0)
              Text(
                '${students.length}/$capacity',
                style: const TextStyle(fontSize: 16),
              ),
          ],
        ),
        actions: [
          if (!isEditMode)
            TextButton(
                onPressed: () {
                  setState(() {
                    isEditMode = true;
                  });
                },
                child: const Text(
                  "Düzenle",
                  style: TextStyle(color: Colors.black),
                )
            )
        ],
      ),
      body: isEditMode ?
        ReorderableListView(
          onReorder: (int oldIndex, int newIndex) {
            setState(() {
              if (newIndex > oldIndex) newIndex--;
              final student = students.removeAt(oldIndex);
              students.insert(newIndex, student);

              for (int i = 0; i < students.length; i++) {
                students[i].sortIndex = i;
              }
            });
          },
          children: List.generate(students.length, (index) {
            final student = students[index];
            return Card(
              key: ValueKey(student.studentId),
              margin: const EdgeInsets.symmetric(vertical: 8, horizontal: 16),
              child: ListTile(
                leading: const Icon(Icons.drag_handle, color: Colors.grey),
                title: Text(
                  student.name,
                  style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                ),
                subtitle: Text('Veli Tel No\t ${student.parentPhone}'),
                onTap: () => _showStudentDetailsDialog(student),
              ),
            );
          })
        ) : ListView.builder(
          itemCount: students.length,
          itemBuilder: (context, index) {
            final student = students[index];
            return Card(
              margin: const EdgeInsets.symmetric(vertical: 8, horizontal: 16),
              child: ListTile(
                title: Text(
                  student.name,
                  style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                ),
                subtitle: Text('Veli Tel No\t ${student.parentPhone}'),
                onTap: () => _showStudentDetailsDialog(student),
              ),
            );
          }),
      bottomNavigationBar: isEditMode ?
        BottomAppBar(
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: [
              TextButton(
                onPressed: () {
                  setState(() {
                    isEditMode = false;
                  });
                },
                child: const Text('İptal'),
              ),
              TextButton(
                onPressed: () async {
                  setState(() {
                    isEditMode = false;
                  });
                  await _handleUpdateOrder();
                },
                child: const Text('Onayla'),
              ),
            ],
          ),
        ) : BottomNavigationBar(
          currentIndex: 1,
          onTap: (index) {
            if (index == 0) {
              Navigator.pushReplacement(
                context,
                MaterialPageRoute(builder: (context) => const ServiceDashboard()),
              );
            } else if (index == 2) {
              /*Navigator.pushReplacement(
                context,
                MaterialPageRoute(builder: (context) => const MessagesScreen()),
              );*/
            }
          },
          type: BottomNavigationBarType.fixed,
          selectedItemColor: Colors.blueAccent,
          unselectedItemColor: Colors.grey,
          items: const [
            BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Ana Sayfa'),
            BottomNavigationBarItem(icon: Icon(Icons.list), label: 'Öğrenci Listesi'),
            BottomNavigationBarItem(icon: Icon(Icons.notifications), label: 'Bildirimler'),
          ],
      ),
    );
  }
}