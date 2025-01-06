import 'dart:convert';
import 'dart:developer';
import 'package:mobil/src/api/api_client.dart';
import 'package:mobil/src/api/endpoints.dart';
import 'package:mobil/src/models/excluded_dates_model.dart';
import 'package:mobil/src/models/student_model.dart';
import 'package:mobil/src/models/update_parent_model.dart';
import 'package:mobil/src/models/update_student_model.dart';

class ParentService {
  final ApiClient _apiClient = ApiClient();

  Future<Map<String, dynamic>?> getParentInfo() async {
    try {
      final response = await _apiClient.get(
        Endpoints.getParentInfo,
      );
      return response.data['data'];
    } catch (e) {
      log("Error fetching parent info: $e");
      return null;
    }
  }

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
      print(fetchedStudents);
      return fetchedStudents.where((e) => e != null).toList();
    } catch (e) {
      log("Error fetching students: $e");
      return [];
    }
  }

  Future<List<DateTime>?> getExcludedDates(int? studentId) async {
    try {
      final response = await _apiClient.get(Endpoints.getExcludedDates(studentId!));
      if (response.data != null) {
        List<dynamic> days = response.data["\$values"];
        List<DateTime> fetchedDays = days.map((day) => DateTime.parse(day)).toList();
        return fetchedDays;
      } else {
        return null;
      }
    } catch (e) {
      throw Exception("Error setting excluded dates: $e");
    }
  }

  Future<bool> updateParent(UpdateParentModel updatedParent) async {
    try {
      await _apiClient.put(
          Endpoints.updateParent, updatedParent.toJson()
      );
      return true;
    } catch (e) {
      print("Error updating parent: $e");
      return false;
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

  Future<bool> setExcludedDates(ExcludedDatesModel excludedDates, int studentId) async {
    try {
      await _apiClient.post(
          Endpoints.setExcludedDates(studentId), excludedDates.toJson()
      );
      return true;
    } catch (e) {
      print("Error setting excluded dates: $e");
      return false;
    }
  }
}