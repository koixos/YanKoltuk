import 'package:flutter/material.dart';

class SignupPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final isKeyboardVisible = MediaQuery.of(context).viewInsets.bottom != 0;

    return Scaffold(
      appBar: AppBar(
        title: Text('Sign Up'), // AppBar başlığı
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
                        'Hadi Başlayalım.',
                    style: TextStyle(
                      fontSize: 35, // Büyük boyut
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
                  decoration: InputDecoration(
                    labelText: 'Username',
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
                    // Sign Up işlemi burada yapılabilir
                  },
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.lightBlue, // Yeşil renk
                  ),
                  child: Text('Sign Up', style: TextStyle(color: Colors.white)), // Yazı rengi beyaz
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
                    'assets/signup.png',
                    width: 120, // Resmin genişliği
                    height: 120, // Resmin yüksekliği
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
