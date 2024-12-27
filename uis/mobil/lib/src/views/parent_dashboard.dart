import 'package:flutter/material.dart';
import 'package:mobil/src/models/student_model.dart';
import 'package:mobil/src/models/student_service_model.dart';
import 'package:mobil/src/models/update_parent_model.dart';
import 'package:mobil/src/models/update_student_model.dart';
import 'package:mobil/src/service/parent_service.dart';
import 'package:mobil/src/shared/secure_storage.dart';
import 'package:mobil/src/views/login_page.dart';
import 'package:mobil/src/widgets/calendar_page.dart';

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
  Map<String, dynamic>? parent;
  String selectedPlate = "Seçiniz";

  @override
  void initState() {
    super.initState();
    _fetchParentInfo();
    _fetchStudents();
    _fetchServicePlates();
  }

  Future<void> _fetchParentInfo() async {
    final fetchedData = await _parentService.getParentInfo();
    if (fetchedData != null) {
      setState(() {
        parent = fetchedData;
      });
    }
  }

  Future<void> _fetchStudents() async {
    final fetchedStudents = await _parentService.getStudents();
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
    }
  }

  Future<void> _handleUpdateParent(String phone, String address) async {
    final parentUpdated = UpdateParentModel(
      phone: phone,
      address: address
    );
    final response = await _parentService.updateParent(parentUpdated);
    if (response) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Bilgileriniz başarıyla güncellendi.")),
      );

      Future.delayed(const Duration(seconds: 2), () {
        Navigator.pushNamed(context, '/parentDashboard');
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Profil bilgisi düzenlenemedi. Lütfen girdiğiniz bilgileri kontrol ediniz.")),
      );
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

  Future<void> _handleEditStudent(int? studentId, String plate) async {
    final studentUpdated = UpdateStudentModel(
      plate: plate,
    );
    final response = await _parentService.editStudent(studentUpdated, studentId!);
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

  Future<void> _handleDeleteStudent(int? studentId) async {
    final response = await _parentService.deleteStudent(studentId!);
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

  Future<void> _handleLogout(BuildContext context) async {
    try {
      await SecureStorage.deleteUserInfo();

      Navigator.pushAndRemoveUntil(
          context,
          MaterialPageRoute(builder: (context) => LoginPage()),
          (route) => false,
      );

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Başarıyla çıkış yaptınız!')),
      );
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Çıkış sırasında hata oluştu: $e')),
      );
    }
  }

  void _showProfilePanel(BuildContext context) {
    bool isEditing = false;

    _fetchParentInfo();

    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.vertical(top: Radius.circular(16)),
      ),
      builder: (BuildContext context) {
        return StatefulBuilder(
          builder: (BuildContext context, StateSetter setModalState) {
            return Padding(
              padding: EdgeInsets.only(
                top: 16,
                left: 16,
                right: 16,
                bottom: MediaQuery.of(context).viewInsets.bottom + 16,
              ),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  Text(
                    "Veli Bilgileri",
                    style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
                  ),
                  SizedBox(height: 16),
                  if (parent != null) ...[
                    ListTile(
                      title: Text("İsim Soyisim"),
                      subtitle: Text(parent?['name']),
                    ),
                    if (!isEditing) ...[
                      ListTile(
                        title: Text("Telefon Numarası"),
                        subtitle: Text(parent?['phone']),
                      ),
                      ListTile(
                        title: Text("Adres"),
                        subtitle: Text(parent?['address']),
                      ),
                    ] else ...[
                      Padding(
                        padding: const EdgeInsets.symmetric(vertical: 8.0),
                        child: TextField(
                          decoration: InputDecoration(
                            labelText: "Telefon Numarası",
                            border: OutlineInputBorder(),
                          ),
                          controller: TextEditingController(text: parent?['phone']),
                          onChanged: (value) {
                            parent?['phone'] = value;
                          },
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.symmetric(vertical: 8.0),
                        child: TextField(
                          decoration: InputDecoration(
                            labelText: "Adres",
                            border: OutlineInputBorder(),
                          ),
                          controller: TextEditingController(text: parent?['address']),
                          onChanged: (value) {
                            parent?['address'] = value;
                          },
                        ),
                      ),
                    ],
                    SizedBox(height: 16),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                      children: [
                        if (!isEditing)
                          ElevatedButton.icon(
                            onPressed: () {
                              setModalState(() {
                                isEditing = true;
                              });
                            },
                            icon: Icon(Icons.edit),
                            label: Text("Bilgileri Güncelle"),
                          ),
                        if (isEditing)
                          ElevatedButton.icon(
                            onPressed: () {
                              _handleUpdateParent(parent?['phone'], parent?['address']);
                              setModalState(() {
                                isEditing = false;
                              });
                            },
                            icon: Icon(Icons.save),
                            label: Text("Kaydet"),
                          ),
                      ],
                    ),
                  ],
                  SizedBox(height: 16),
                  ElevatedButton.icon(
                    onPressed: () => _showLogoutDialog(),
                    icon: Icon(Icons.logout),
                    label: Text("Çıkış Yap"),
                    style: ElevatedButton.styleFrom(
                      foregroundColor: Colors.white, backgroundColor: Colors.red,
                    ),
                  ),
                ],
              ),
            );
          },
        );
      },
    );
  }

  void _showAddStudentDialog(BuildContext context) {
    _fetchServicePlates();

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
              contentPadding: EdgeInsets.only(top: 20, left: 24, right: 24, bottom: 10),
              actionsPadding: EdgeInsets.only(bottom: 10),
              actionsAlignment: MainAxisAlignment.center,
              actions: [
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    TextButton(
                      onPressed: () => _handleEditStudent(student.studentId, updatedPlate),
                      child: const Text("Kaydet"),
                    ),
                    SizedBox(width: 20),
                    TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: const Text("İptal"),
                    ),
                  ],
                )
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

  void _showLogoutDialog() {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: Text("Çıkış Yap"),
          content: Text("Çıkış yapmak istediğinize emin misiniz?"),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: Text("Hayır"),
            ),
            TextButton(
              onPressed: () => _handleLogout(context),
              child: Text("Evet"),
            ),
          ],
        );
      },
    );
  }

  void _onSetExcludedDates(StudentServiceModel student) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => CalendarPage(
          studentName: student.name,
          studentId: student.studentId,
          excludedStartDate: student.excludedStartDate,
          excludedEndDate: student.excludedEndDate,
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: false,
        title: const Text("Öğrenci Listesi"),
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
                  onEdit: () => _showEditStudentDialog(students[index]),
                  onDelete: () => _showDeleteStudentDialog(students[index]),
                  onSetExcludedDates: () => _onSetExcludedDates(students[index]),
                );
              },
            ),
      bottomNavigationBar: BottomNavigationBar(
        onTap: (index) {
          if (index == 0) {
            _showAddStudentDialog(context);
          } else if (index == 1) {
            _showProfilePanel(context);
          }
        },
        items: [
          BottomNavigationBarItem(
            icon: Icon(Icons.add),
            label: "Öğrenci Ekle",
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.person),
            label: "Profil",
          ),
        ],
      ),
    );
  }
}