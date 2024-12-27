import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class SecureStorage {
  static const _storage = FlutterSecureStorage();

  static Future<void> saveUserInfo(String token, String role) async {
    await _storage.write(key: 'auth_token', value: token);
    await _storage.write(key: 'role', value: role);
  }

  static Future<String?> getToken() async {
    return await _storage.read(key: 'auth_token');
  }

  static Future<String?> getRole() async {
    return await _storage.read(key: 'role');
  }

  static Future<void> deleteUserInfo() async {
    await _storage.delete(key: 'auth_token');
    await _storage.delete(key: 'role');
  }
}