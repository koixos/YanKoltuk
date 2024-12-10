import 'package:flutter/material.dart';
import 'package:mobil/src/views/login_page.dart';
import 'package:mobil/src/views/parent_dashboard.dart';
import 'package:mobil/src/views/service_dashboard.dart';
//import 'package:mobil/src/views/login_page.dart';
import 'package:mobil/src/views/signup_page.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      initialRoute: '/login',
      routes: {
        '/login': (context) => LoginPage(),
        '/signup': (context) => SignupPage(),
        '/parentDashboard': (context) => ParentDashboard(),
        //'/serviceDashboard': (context) => ServiceDashboard(),
      },
    );
  }
}