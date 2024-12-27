class LoginModel {
  final String username;
  final String passwd;

  LoginModel({
    required this.username,
    required this.passwd
  });

  Map<String, dynamic> toJson() {
    return {
      'username': username,
      'password': passwd
    };
  }
}