import 'package:dio/dio.dart';
import 'package:mobil/src/shared/secure_storage.dart';

class ApiClient {
  final Dio _dio = Dio(
    BaseOptions(
      baseUrl: "http://10.0.2.2:5139/api",
      responseType: ResponseType.json,
      connectTimeout: const Duration(milliseconds: 5000),
      receiveTimeout: const Duration(milliseconds: 3000),
    ),
  );

  ApiClient() {
    _dio.interceptors.add(
      InterceptorsWrapper(
        onRequest: (options, handler) async {
          final token = await SecureStorage.getToken();
          if (token != null) {
            options.headers['Authorization'] = 'Bearer $token';
          }
          return handler.next(options);
        },
        onError: (DioException e, handler) {
          if (e.response?.statusCode == 401) {
            print("Unauthorized - redirect to login");
          }
          return handler.next(e);
        }
      )
    );
  }

  Future<Response> get(String endpoint) async {
    try {
      return await _dio.get(endpoint);
    } catch (e) {
      throw Exception("GET request failed: $e");
    }
  }

  Future<Response> post(String endpoint, Map<String, dynamic> data) async {
    try {
      return await _dio.post(endpoint, data: data);
    } on DioException catch (e) {
      if (e.response != null) {
        throw Exception("POST failed: ${e.response?.data}");
      } else {
        throw Exception("POST request error: ${e.message}");
      }
    }
  }

  Future<Response> put(String endpoint, dynamic data) async {
    try {
      return await _dio.put(endpoint, data: data);
    } on DioException catch (e) {
      if (e.response != null) {
        throw Exception("PUT failed: ${e.response?.data}");
      } else {
        throw Exception("PUT request error: ${e.message}");
      }
    }
  }

  Future<Response> delete(String endpoint) async {
    try {
      return await _dio.delete(endpoint);
    } on DioException catch (e) {
      if (e.response != null) {
        throw Exception("DELETE failed: ${e.response?.data}");
      } else {
        throw Exception("DELETE request error: ${e.message}");
      }
    }
  }
}