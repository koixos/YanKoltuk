import 'dart:developer';
import 'package:mobil/src/api/api_client.dart';
import 'package:mobil/src/api/endpoints.dart';
import 'package:mobil/src/models/student_model.dart';
import 'package:mobil/src/shared/secure_storage.dart';

class ParentService {
  final ApiClient _apiClient = ApiClient();

  Future<List<String>?> getServicePlates() async {
    try {
      final response = await _apiClient.get(
          Endpoints.getServicePlates,
      );
      return List<String>.from(response.data["\$values"]);
    } catch (e) {
      log("Error fetching service plates: $e");
      return [];
    }
  }

  Future<List<dynamic>?> getStudents() async {
    try {
      final response = await _apiClient.get(
        Endpoints.getStudents
      );
      print("TEST ${List<dynamic>.from(response.data["data"]["\$values"])}");
      List<dynamic> fetchedStudents = List<dynamic>.from(response.data["data"]["\$values"]);
      return fetchedStudents.where((e) => e != null).toList();
    } catch (e) {
      log("Error fetching students: $e");
      return [];
    }
  }

  Future<bool> addStudent(StudentModel student) async {
    try {
      await _apiClient.post(
        Endpoints.addStudent, student.toJson()
      );
      return true;
    } catch (e) {
      log("Error adding student: $e");
      return false;
    }
  }

  /*
  void _editParentInfo() {
    String? phoneNumber = parent?.phoneNumber;
    String? address = parent?.address;

    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: const Text("Veli Bilgilerini Düzenle"),
          content: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              TextField(
                decoration: const InputDecoration(labelText: "Telefon Numarası"),
                controller: TextEditingController(text: phoneNumber),
                onChanged: (value) => phoneNumber = value,
              ),
              TextField(
                decoration: const InputDecoration(labelText: "Adres"),
                controller: TextEditingController(text: address),
                onChanged: (value) => address = value,
              ),
            ],
          ),
          actions: [
            TextButton(
              onPressed: () {
                setState(() {
                  parent?.phoneNumber = phoneNumber!;
                  parent?.address = address!;
                });
                Navigator.pop(context);
              },
              child: const Text("Kaydet"),
            ),
          ],
        );
      },
    );
  }

  void _editStudent(Student student) {
    String selectedEditPlate = student.plateNumber;

    showDialog(
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (context, setDialogState) {
            return AlertDialog(
              title: const Text("Servis Plakası Güncelle"),
              content: Column(
                mainAxisSize: MainAxisSize.min,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  // Diğer bilgilerin sadece okunabilir olarak gösterimi
                  TextField(
                    decoration: const InputDecoration(labelText: "İsim Soyisim"),
                    controller: TextEditingController(text: student.name),
                    readOnly: true,
                  ),
                  TextField(
                    decoration: const InputDecoration(labelText: "Öğrenci Numarası"),
                    controller: TextEditingController(text: student.studentNumber),
                    readOnly: true,
                  ),
                  TextField(
                    decoration: const InputDecoration(labelText: "TC Kimlik Numarası"),
                    controller: TextEditingController(text: student.tcNumber),
                    readOnly: true,
                  ),
                  const SizedBox(height: 10),
                  const Text("Servis Plakasını Güncelle:"),
                  DropdownButton<String>(
                    value: selectedEditPlate,
                    onChanged: (String? newValue) {
                      setDialogState(() {
                        selectedEditPlate = newValue!;
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
                  onPressed: () {
                    setState(() {
                      student.plateNumber = selectedEditPlate;
                    });
                    Navigator.pop(context);
                  },
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

  void _deleteStudent(Student student) {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: const Text("Öğrenci Sil"),
          content: Text("${student.name} isimli öğrenci silinecek. Onaylıyor musunuz?"),
          actions: [
            TextButton(
              onPressed: () {
                setState(() {
                  students.remove(student);
                });
                Navigator.pop(context);
              },
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
   */
}