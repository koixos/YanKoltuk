import 'package:flutter/material.dart';
import 'login_page.dart';

class SplashScreen extends StatefulWidget {
  const SplashScreen({super.key});

  @override
  _SplashScreenState createState() => _SplashScreenState();
}

class _SplashScreenState extends State<SplashScreen> {
  @override
  void initState() {
    super.initState();
    // Splash ekranını 3 saniye gösterip LoginPage'e geçiş
    Future.delayed(const Duration(seconds: 2), () {
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (context) => LoginPage()),
      );
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.lightBlue, // Splash ekran arka plan rengi
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Image.asset(
              'assets/splash.png', // Splash ekranında gösterilecek resim
              width: 150, // Resmin genişliği
              height: 150, // Resmin yüksekliği
            ),
            const SizedBox(height: 20),
            const Text(
              'Yan Koltuk', // Splash ekranında gösterilecek metin
              style: TextStyle(
                fontSize: 40,
                fontWeight: FontWeight.bold,
                color: Colors.white, // Metin rengi
              ),
            ),
            const SizedBox(height: 20), // Metin ile çember arasında boşluk
            const SizedBox(
              width: 20, // Yükleme çemberinin genişliği
              height: 20, // Yükleme çemberinin yüksekliği
              child: CircularProgressIndicator(
                valueColor: AlwaysStoppedAnimation<Color>(Colors.white), // Yükleme çemberinin rengi
              ),
            ),
          ],
        ),
      ),
    );
  }
}
