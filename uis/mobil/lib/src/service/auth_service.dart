import 'dart:developer';
import 'package:mobil/src/api/api_client.dart';
import 'package:mobil/src/api/endpoints.dart';
import 'package:mobil/src/models/login_model.dart';
import 'package:mobil/src/models/parent_signup_model.dart';

class AuthService {
  final ApiClient _apiClient = ApiClient();

  Future<Map<String, dynamic>?> login(LoginModel login) async {
    try {
      final response = await _apiClient.post(
        Endpoints.login, login.toJson()
      );

      if (response.statusCode == 200 || response.statusCode == 201) {
        return response.data;
      } else {
        log("Login failed: ${response.data}");
        return null;
      }
    } catch (e) {
      log("Login error: $e");
      return null;
    }
  }

  Future<bool> signup(ParentSignupModel parent) async {
    try {
      final response = await _apiClient.post(
        Endpoints.signupParent, parent.toJson()
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