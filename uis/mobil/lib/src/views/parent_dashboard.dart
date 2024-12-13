import 'package:flutter/material.dart';
import 'package:mobil/src/models/parent_model.dart';
import 'package:mobil/src/models/student_model.dart';
import 'package:mobil/src/models/student_service_model.dart';
import 'package:mobil/src/models/update_student_model.dart';
import 'package:mobil/src/service/parent_service.dart';

import '../widgets/student_card.dart';

class ParentDashboard extends StatefulWidget {
  const ParentDashboard({super.key});

  @override
  State<ParentDashboard> createState() => _ParentDashboardState();
}

class _ParentDashboardState extends State<ParentDashboard> {
  final ParentService _parentService = ParentService();

  final TextEditingController idNoController = TextEditingController();
  final TextEditingController nameController = TextEditingController();
  final TextEditingController schoolNoController = TextEditingController();

  List<StudentServiceModel> students = [];
  List<String> servicePlates = [];
  ParentModel? parent;
  String selectedPlate = "Seçiniz";

  @override
  void initState() {
    super.initState();
    _fetchStudents();
    _fetchServicePlates();
  }

  Future<void> _fetchStudents() async {
    final fetchedStudents = await _parentService.getStudents();
    print(fetchedStudents);
    if (fetchedStudents != null) {
      setState(() {
        students = fetchedStudents.map((e) => StudentServiceModel.fromJson(e)).toList();
      });
    }
  }

  Future<void> _fetchServicePlates() async {
    final fetchedPlates = await _parentService.getServicePlates();
    if (fetchedPlates != null) {
      setState(() {
        servicePlates = fetchedPlates;
      });
    } else {
      servicePlates = [];
    }
  }

  Future<void> _handleAddStudent(BuildContext context) async {
    if (selectedPlate == 'Seçiniz'
        || idNoController.text.isEmpty
        || nameController.text.isEmpty
        || schoolNoController.text.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Lütfen tüm alanları doldurun.")),
      );
      return;
    }

    final student = StudentModel(
      idNo: idNoController.text,
      name: nameController.text,
      schoolNo: schoolNoController.text,
      plate: selectedPlate,
    );

    final response = await _parentService.addStudent(student);
    print(response);
    if (response) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Öğrenci başarıyla eklendi.")),
      );

      Future.delayed(const Duration(seconds: 2), () {
        Navigator.pushNamed(context, '/parentDashboard');
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Öğrenci eklenemedi. Lütfen girdiğiniz bilgileri kontrol ediniz.")),
      );
    }
  }

  Future<void> _handleEditStudent(int studentId, String plate) async {
    final studentUpdated = UpdateStudentModel(
      plate: plate,
    );
    final response = await _parentService.editStudent(studentUpdated, studentId);
    if (response) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Öğrenci başarıyla güncellendi.")),
      );

      Future.delayed(const Duration(seconds: 2), () {
        Navigator.pushNamed(context, '/parentDashboard');
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Öğrenci bilgisi düzenlenemedi. Lütfen girdiğiniz bilgileri kontrol ediniz.")),
      );
    }
  }

  Future<void> _handleDeleteStudent(int studentId) async {
    final response = await _parentService.deleteStudent(studentId);
    if (response) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Öğrenci başarıyla silindi.")),
      );

      Future.delayed(const Duration(seconds: 2), () {
        Navigator.pushNamed(context, '/parentDashboard');
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text(
            "Öğrenci silinemedi.")),
      );
    }
  }

  void _showAddStudentDialog(BuildContext context) {
    showDialog(
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (context, setDialogState) {
            return AlertDialog(
              title: const Text("Öğrenci Ekle"),
              content: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  TextField(
                    controller: idNoController,
                    decoration: const InputDecoration(
                        labelText: "T.C. No"),
                  ),
                  TextField(
                    controller: nameController,
                    decoration: const InputDecoration(
                        labelText: "Ad Soyad"),
                  ),
                  TextField(
                    controller: schoolNoController,
                    decoration: const InputDecoration(
                        labelText: "Okul No"),
                  ),
                  const SizedBox(height: 10),
                  const Text("Servis Plakasını Seçin:"),
                  DropdownButton<String>(
                    value: selectedPlate,
                    onChanged: (String? newValue) {
                      setDialogState(() {
                        selectedPlate = newValue!;
                      });
                    },
                    items: ['Seçiniz', ...servicePlates].map<DropdownMenuItem<String>>((
                        String plate) {
                      return DropdownMenuItem<String>(
                        value: plate,
                        child: Text(plate),
                      );
                    }).toList(),
                  ),
                ],
              ),
              actions: [
                TextButton(
                  onPressed: () => Navigator.pop(context),
                  child: const Text("İptal"),
                ),
                TextButton(
                    onPressed: () => _handleAddStudent(context),
                    child: const Text("Ekle"),
                )
              ],
            );
          },
        );
      },
    );
  }

  void _showEditStudentDialog(StudentServiceModel student) {
    var updatedPlate = student.plate;

    final TextEditingController idNoController = TextEditingController(text: student.idNo);
    final TextEditingController nameController = TextEditingController(text: student.name);
    final TextEditingController schoolNoController = TextEditingController(text: student.schoolNo);

    showDialog(
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (context, setDialogState) {
            return AlertDialog(
              title: const Text("Servis Bilgisini Güncelle"),
              content: Column(
                mainAxisSize: MainAxisSize.min,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  TextField(
                    decoration: const InputDecoration(labelText: "T.C. Kimlik No"),
                    controller: idNoController,
                    readOnly: true,
                  ),
                  TextField(
                    decoration: const InputDecoration(labelText: "Ad Soyad"),
                    controller: nameController,
                    readOnly: true,
                  ),
                  TextField(
                    decoration: const InputDecoration(labelText: "Okul No"),
                    controller: schoolNoController,
                    readOnly: true,
                  ),
                  const SizedBox(height: 10),
                  const Text("Servis Plakasını Güncelle:"),
                  DropdownButton<String>(
                    value: updatedPlate,
                    onChanged: (String? newValue) {
                      setDialogState(() {
                        updatedPlate = newValue!;
                      });
                    },
                    items: servicePlates.map<DropdownMenuItem<String>>((String plate) {
                      return DropdownMenuItem<String>(
                        value: plate,
                        child: Text(plate),
                      );
                    }).toList(),
                  ),
                ],
              ),
              actions: [
                TextButton(
                  onPressed: () => _handleEditStudent(student.studentId, updatedPlate),
                  child: const Text("Kaydet"),
                ),
                TextButton(
                  onPressed: () => Navigator.pop(context),
                  child: const Text("İptal"),
                ),
              ],
            );
          },
        );
      },
    );
  }

  void _showDeleteStudentDialog(StudentServiceModel student) {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: const Text("Öğrenciyi Sil"),
          content: Text("${student.name} isimli öğrenci silinecek. Onaylıyor musunuz?"),
          actions: [
            TextButton(
              onPressed: () => _handleDeleteStudent(student.studentId),
              child: const Text("Evet"),
            ),
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: const Text("Hayır"),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Öğrenci Listesi"),
        actions: [
          Builder(
            builder: (BuildContext context) {
              return IconButton(
                icon: const Icon(Icons.arrow_back_ios_sharp),
                onPressed: () {
                  Scaffold.of(context).openEndDrawer();
                },
              );
            },
          ),
        ],
      ),
      body: students.isEmpty
          ? const Center(
              child: Text(
                "Öğrenci eklemek için + butonuna tıklayınız.",
                style: TextStyle(color: Colors.grey, fontSize: 20),
                textAlign: TextAlign.center,
              ),
            )
          : ListView.builder(
              itemCount: students.length,
              itemBuilder: (context, index) {
                return StudentCard(
                  student: students[index],
                  onStatusChanged: (value) {
                    //setState(() {
                      //students[index].isComingTomorrow = value;
                    //});
                  },
                  onEdit: () => _showEditStudentDialog(students[index]),
                  onDelete: () => _showDeleteStudentDialog(students[index]),
                );
              },
            ),
      endDrawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: [
            const DrawerHeader(
              decoration: BoxDecoration(color: Colors.blue),
              child: Text(
                "Veli Bilgileri",
                style: TextStyle(color: Colors.white, fontSize: 24),
              ),
            ),
            if (parent != null) ...[
              ListTile(
                title: const Text("İsim Soyisim"),
                subtitle: Text(parent!.name),
              ),
              ListTile(
                title: const Text("Telefon Numarası"),
                subtitle: Text(parent!.phone),
                /*trailing: IconButton(
                  icon: const Icon(Icons.edit),
                  onPressed: _editParentInfo,
                ),*/
              ),
              ListTile(
                title: const Text("Adres"),
                subtitle: Text(parent!.address),
                /*trailing: IconButton(
                  icon: const Icon(Icons.edit),
                  onPressed: _editParentInfo,
                ),*/
              ),
            ],
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _showAddStudentDialog(context),
        foregroundColor: Colors.white,
        backgroundColor: Colors.blue,
        child: const Icon(Icons.add),
      ),
    );
  }
}