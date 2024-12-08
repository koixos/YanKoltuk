import 'dart:developer';

import 'package:mobil/src/api/api_client.dart';
import 'package:mobil/src/api/endpoints.dart';

class AuthService {
  final ApiClient _apiClient = ApiClient();

  Future<bool> signup(String name, String idNo, String phone, String address, String passwd) async {
    try {
      final response = await _apiClient.post(
        Endpoints.signupParent,
        {
          'name': name,
          'idNo': idNo,
          'phone': phone,
          'address': address,
          'password': passwd
        },
      );

      if (response.statusCode == 200 || response.statusCode == 201) {
        return true;
      } else {
        log("Signup failed: ${response.data}");
        return false;
      }
    } catch (e) {
      log("Signup error: $e");
      return false;
    }
  }
}