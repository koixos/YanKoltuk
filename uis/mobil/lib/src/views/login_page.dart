import 'package:flutter/material.dart';
import 'package:mobil/src/models/login_model.dart';
import 'package:mobil/src/service/auth_service.dart';
import 'package:mobil/src/views/parent_views/parent_dashboard.dart';
import 'package:mobil/src/views/service_views/service_dashboard.dart';
import '../shared/secure_storage.dart';
import './parent_views/signup_page.dart';

class LoginPage extends StatelessWidget {
  final AuthService _authService = AuthService();

  LoginPage({super.key});

  final TextEditingController usernameController = TextEditingController();
  final TextEditingController passwdController = TextEditingController();

  void _handleLogin(BuildContext context) async {
    final login = LoginModel(
        username: usernameController.text,
        passwd: passwdController.text,
    );

    final response = await _authService.login(login);

    if (response != null) {
      final token = response['token'];
      final role = response['role'];

      await SecureStorage.saveUserInfo(token, role);

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Giriş başarılı! Ana sayfaya yönlendiriliyorsunuz.")),
      );

      Future.delayed(const Duration(seconds: 2), ()
      {
        Widget target = LoginPage();

        if (role == 'Parent') {
          target = const ParentDashboard();
        } else if (role == 'Service') {
          target = const ServiceDashboard();
        } else {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(content: Text("Giriş başarısız. Tekrar deneyin.")),
          );
        }

        Navigator.pushReplacement(
          context,
          PageRouteBuilder(
            transitionDuration: Duration(milliseconds: 400),
            pageBuilder: (context, animation, secondaryAnimation) => target,
            transitionsBuilder: (context, animation, secondaryAnimation, child) {
              const begin = Offset(1.0, 0.0);
              const end = Offset.zero;
              final tween = Tween(begin: begin, end: end);
              final offsetAnimation = animation.drive(tween);

              return SlideTransition(
                position: offsetAnimation,
                child: child,
              );
            },
          ),
        );
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    final isKeyboardVisible = MediaQuery.of(context).viewInsets.bottom != 0;

    return Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: false,
        title: const Text('Giriş Yap'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Stack(
          children: [
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                const SizedBox(height: 40),
                const Align(
                  alignment: Alignment.centerLeft,
                  child: Text(
                    'Merhaba,\nTekrar Hoş Geldiniz.',
                    style: TextStyle(
                      fontSize: 40,
                      fontWeight: FontWeight.bold,
                      color: Colors.black,
                    ),
                  ),
                ),
                const SizedBox(height: 50),
                TextField(
                  controller: usernameController,
                  decoration: const InputDecoration(
                    labelText: 'Kullanıcı Adı',
                    labelStyle: TextStyle(color: Colors.black),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.lightBlue),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.lightBlue),
                    ),
                  ),
                ),
                const SizedBox(height: 16),
                TextField(
                  controller: passwdController,
                  obscureText: true,
                  decoration: const InputDecoration(
                    labelText: 'Şifre',
                    labelStyle: TextStyle(color: Colors.black),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.lightBlue),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.lightBlue),
                    ),
                  ),
                ),
                const SizedBox(height: 16),
                ElevatedButton(
                  onPressed: () => _handleLogin(context),
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.lightBlue,
                  ),
                  child: const Text('Giriş Yap', style: TextStyle(color: Colors.white)),
                ),
                TextButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => SignupPage()),
                    );
                  },
                  child: const Text(
                    'Kayıtlı bir hesabın yok mu? Kayıt ol.',
                    style: TextStyle(color: Colors.lightBlue),
                  ),
                ),
              ],
            ),
            Visibility(
              visible: !isKeyboardVisible,
              child: Positioned(
                top: 20,
                left: 0,
                right: 0,
                child: Center(
                  child: Image.asset(
                    'assets/login.png',
                    width: 150,
                    height: 150,
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
