import 'dart:developer';
import 'package:mobil/src/api/api_client.dart';
import 'package:mobil/src/api/endpoints.dart';
import 'package:mobil/src/models/student_model.dart';
import 'package:mobil/src/models/update_student_model.dart';
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

  Future<bool> editStudent(UpdateStudentModel updatedStudent, int studentId) async {
    try {
      await _apiClient.put(
          Endpoints.editStudent(studentId), updatedStudent.toJson()
      );
      return true;
    } catch (e) {
      print("Error updating student: $e");
      return false;
    }
  }

  Future<bool> deleteStudent(int studentId) async {
    try {
      await _apiClient.delete(
          Endpoints.deleteStudent(studentId)
      );
      return true;
    } catch (e) {
      print("Error deleting student: $e");
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


   */
}