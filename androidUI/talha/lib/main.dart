import 'package:flutter/material.dart';
import 'login_page.dart';
import 'signup_page.dart';
import 'splash_screen.dart';
import 'parent_home_page.dart'; // Yeni eklenen dosya

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Login & Signup',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: ServiceTrackingPage(), // SplashScreen yerine öğrenci takip sayfasına yönlendirme
    );
  }
}
