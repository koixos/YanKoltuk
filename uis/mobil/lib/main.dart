import 'package:flutter/material.dart';
import 'package:mobil/src/views/login_page.dart';
import 'package:mobil/src/views/parent_dashboard.dart';
import 'package:mobil/src/views/service_dashboard.dart';
import 'package:mobil/src/views/service_student_list.dart';
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
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      initialRoute: '/login',
      routes: {
        '/login': (context) => LoginPage(),
        '/signup': (context) => SignupPage(),
        '/parentDashboard': (context) => ParentDashboard(),
        '/serviceDashboard': (context) => ServiceDashboard(),
        '/serviceStudentList': (context) => ServiceStudentList(capacity: 0),
      },
    );
  }
}