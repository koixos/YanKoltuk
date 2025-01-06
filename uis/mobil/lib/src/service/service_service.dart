import 'dart:developer';

import 'package:mobil/src/models/update_driver_note_model.dart';
import 'package:mobil/src/models/update_student_order_model.dart';
import 'package:mobil/src/models/update_student_status_model.dart';

import '../api/api_client.dart';
import '../api/endpoints.dart';

class ServiceService {
  final ApiClient _apiClient = ApiClient();

  Future<Map<String, dynamic>?> getServiceInfo() async {
    try {
      final response = await _apiClient.get(
        Endpoints.getServiceInfo,
      );
      return response.data['data'];
    } catch (e) {
      log("Error fetching service info: $e");
      return null;
    }
  }

  Future<List<dynamic>?> getStudents() async {
    try {
      final response = await _apiClient.get(
          Endpoints.getStudentsService
      );
      return response.data["data"]["\$values"];
    } catch (e) {
      log("Error fetching students: $e");
      return [];
    }
  }

  Future<List<dynamic>?> getDrivingList() async {
    try {
      final response = await _apiClient.get(
          Endpoints.getDrivingList
      );
      return response.data["data"]["\$values"];
    } catch (e) {
      log("Error fetching students: $e");
      return [];
    }
  }

  Future<bool> editNote(UpdateDriverNoteModel updatedStudent, int? studentId) async {
    try {
      await _apiClient.put(
        Endpoints.editNote(studentId!), updatedStudent.toJson()
      );
      return true;
    } catch (e) {
      print("Error updating note: $e");
      return false;
    }
  }

  Future<bool> updateOrder(List<UpdateStudentOrderModel> updatedList) async {
    try {
      await _apiClient.put(
          Endpoints.updateStudentOrder,
          updatedList.map((e) => e.toJson()).toList(),
      );
      return true;
    } catch (e) {
      print("Error updating student order: $e");
      return false;
    }
  }

  Future<bool> updateStatus(int studentId, UpdateStudentStatusModel updatedStudent) async {
    try {
      final response = await _apiClient.put(
        Endpoints.updateStudentStatus(studentId),
        updatedStudent.toJson(),
      );
      print(updatedStudent.toJson());
      return true;
    } catch (e) {
      print("Error updating student status: $e");
      return false;
    }
  }
}