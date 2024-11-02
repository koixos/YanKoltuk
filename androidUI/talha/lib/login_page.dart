import 'package:flutter/material.dart';
import 'signup_page.dart';

class LoginPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final isKeyboardVisible = MediaQuery.of(context).viewInsets.bottom != 0;

    return Scaffold(
      appBar: AppBar(
        title: Text('Login'), // AppBar başlığı
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Stack(
          children: [
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                SizedBox(height: 40), // Resmin üstündeki boşluğu ayarladım
                // "Merhaba" metni sola yaslı
                Align(
                  alignment: Alignment.centerLeft,
                  child: Text(
                    'Merhaba,\n'
                        'Tekrar Hoş Geldiniz.',
                    style: TextStyle(
                      fontSize: 40, // Büyük boyut
                      fontWeight: FontWeight.bold, // Kalın yazı
                      color: Colors.black, // Yeşil renk
                    ),
                  ),
                ),
                SizedBox(height: 50), // "Merhaba" ile email alanı arasında boşluk
                TextField(
                  decoration: InputDecoration(
                    labelText: 'Email',
                    labelStyle: TextStyle(color: Colors.black), // Yeşil renk
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.lightBlue), // Normal durumda yeşil sınır
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.lightBlue), // Odaklandığında yeşil sınır
                    ),
                  ),
                ),
                SizedBox(height: 16),
                TextField(
                  obscureText: true,
                  decoration: InputDecoration(
                    labelText: 'Password',
                    labelStyle: TextStyle(color: Colors.black), // Yeşil renk
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.lightBlue), // Normal durumda yeşil sınır
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.lightBlue), // Odaklandığında yeşil sınır
                    ),
                  ),
                ),
                SizedBox(height: 16),
                ElevatedButton(
                  onPressed: () {
                    // Login işlemi burada yapılabilir
                  },
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.lightBlue, // Yeşil renk
                  ),
                  child: Text('Login', style: TextStyle(color: Colors.white)), // Yazı rengi beyaz
                ),
                TextButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => SignupPage()),
                    );
                  },
                  child: Text(
                    'Don\'t have an account? Sign up',
                    style: TextStyle(color: Colors.lightBlue), // Yeşil renk
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
                    width: 150, // Resmin genişliği
                    height: 150, // Resmin yüksekliği
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
