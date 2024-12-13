import 'package:flutter/material.dart';
import 'package:mobil/src/models/parent_model.dart';
import 'package:mobil/src/service/auth_service.dart';
import 'package:mobil/src/views/login_page.dart';

class SignupPage extends StatelessWidget {
  final AuthService _authService = AuthService();

  SignupPage({super.key});

  final TextEditingController nameController = TextEditingController();
  final TextEditingController idNoController = TextEditingController();
  final TextEditingController phoneController = TextEditingController();
  final TextEditingController addressController = TextEditingController();
  final TextEditingController passwdController = TextEditingController();

  void _handleSignup(BuildContext context) async {
    final parent = ParentModel(
        name: nameController.text,
        idNo: idNoController.text,
        phone: phoneController.text,
        address: addressController.text,
        passwd: passwdController.text,
    );

    final response = await _authService.signup(parent);

    if (response) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Kayıt başarılı! Giriş yapabilirsiniz.")),
      );

      Future.delayed(const Duration(seconds: 2), () {
        Navigator.pushNamed(context, '/login');
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Kayıt başarısız. Tekrar deneyin.")),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    final isKeyboardVisible = MediaQuery.of(context).viewInsets.bottom != 0;

    return Scaffold(
      appBar: AppBar(
        title: const Text('Kayıt Ol'),
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
                    'Merhaba,\n'
                        'Hadi Başlayalım.',
                    style: TextStyle(
                      fontSize: 35,
                      fontWeight: FontWeight.bold,
                      color: Colors.black,
                    ),
                  ),
                ),
                const SizedBox(height: 50),
                TextField(
                  controller: nameController,
                  decoration: const InputDecoration(
                    labelText: 'Ad Soyad',
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
                  controller: idNoController,
                  decoration: const InputDecoration(
                    labelText: 'T.C Kimlik No',
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
                  controller: phoneController,
                  decoration: const InputDecoration(
                    labelText: 'Telefon Numarası',
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
                  controller: addressController,
                  decoration: const InputDecoration(
                    labelText: 'Adres',
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
                  onPressed: () => _handleSignup(context),
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.lightBlue,
                  ),
                  child: const Text('Kayıt Ol', style: TextStyle(color: Colors.white)),
                ),
                TextButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => LoginPage()),
                    );
                  },
                  child: const Text(
                    'Zaten bir hesabın var mı? Giriş yap.',
                    style: TextStyle(color: Colors.lightBlue),
                  ),
                ),
              ],
            ),
            Visibility(
              visible: !isKeyboardVisible,
              child: const Positioned(
                top: 20,
                left: 0,
                right: 0,
                child: Center(
                  /*child: Image.asset(
                    'assets/signup.png',
                    width: 120,
                    height: 120,
                  ),*/
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}