import 'dart:async';

import 'package:flutter/material.dart';
import 'package:mobil/src/service/service_service.dart';
import 'package:mobil/src/views/service_views/driving_list_page.dart';
import 'package:mobil/src/views/service_views/service_student_list.dart';

import '../../shared/secure_storage.dart';
import '../login_page.dart';

class ServiceDashboard extends StatefulWidget {
  const ServiceDashboard({super.key});

  @override
  State<ServiceDashboard> createState() => _ServiceDashboardState();
}

class _ServiceDashboardState extends State<ServiceDashboard> {
  final ServiceService _serviceService = ServiceService();

  final List<String> messages = [
    "Hoşgeldiniz, Keyifli Sürüşler",
    "Lütfen Emniyet Kemerinizi Takınız",
    "Öğrenci İndirip Bindirirken Dikkatli Olun"
  ];

  Map<String, dynamic>? service;
  late PageController _pageController;
  int currentIndex = 0;

  @override
  void initState() {
    super.initState();
    _fetchServiceInfo();

    _pageController = PageController(initialPage: 0);
    Timer.periodic(const Duration(seconds: 3), (timer) {
      currentIndex = (currentIndex + 1) % messages.length;
      _pageController.animateToPage(
        currentIndex,
        duration: const Duration(milliseconds: 500),
        curve: Curves.easeInOut,
      );
    });
  }

  Future<void> _fetchServiceInfo() async {
    final fetchedData = await _serviceService.getServiceInfo();
    if (fetchedData != null) {
      setState(() {
        service = fetchedData;
      });
    }
  }

  Future<void> _handleLogout(BuildContext context) async {
    try {
      await SecureStorage.deleteUserInfo();

      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute(builder: (context) => LoginPage()),
            (route) => false,
      );

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Başarıyla çıkış yaptınız!')),
      );
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Çıkış sırasında hata oluştu: $e')),
      );
    }
  }

  void _showLogoutDialog() {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: Text("Çıkış Yap"),
          content: Text("Çıkış yapmak istediğinize emin misiniz?"),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: Text("Hayır"),
            ),
            TextButton(
              onPressed: () => _handleLogout(context),
              child: Text("Evet"),
            ),
          ],
        );
      },
    );
  }

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xFFFDF5ED),
      appBar: AppBar(
        automaticallyImplyLeading: false,
        backgroundColor: Colors.teal.shade400,
        elevation: 0,
        title: const Text(
          'Yan Koltuk',
          style: TextStyle(
            fontFamily: 'Poppins',
            fontWeight: FontWeight.bold,
            fontSize: 22,
            color: Colors.white,
          ),
        ),
        actions: [
          IconButton(
            icon: const Icon(Icons.exit_to_app, color: Colors.white),
            onPressed: _showLogoutDialog,
          ),
        ],
        bottom: PreferredSize(
          preferredSize: const Size.fromHeight(50),
          child: SizedBox(
            height: 50,
            child: PageView.builder(
              controller: _pageController,
              itemCount: messages.length,
              itemBuilder: (context, index) {
                return Center(
                  child: Text(
                    messages[index],
                    style: const TextStyle(
                      color: Colors.white70,
                      fontSize: 18,
                      fontFamily: 'Poppins',
                    ),
                  ),
                );
              },
            ),
          ),
        ),
      ),
      body: service == null
          ? const Center(child: CircularProgressIndicator())
          : SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 20),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 20),
              GridView.count(
                physics: const NeverScrollableScrollPhysics(),
                shrinkWrap: true,
                crossAxisCount: 2,
                crossAxisSpacing: 16,
                mainAxisSpacing: 16,
                children: [
                  _buildInfoCard(
                    icon: Icons.person,
                    title: 'Şoför Bilgileri',
                    description:
                    '${service?["driverName"]}\nTel: ${service?["driverPhone"]}',
                    color: Colors.orangeAccent,
                  ),
                  _buildInfoCard(
                    icon: Icons.support_agent,
                    title: 'Hostes Bilgileri',
                    description:
                    '${service?["stewardessName"]}\nTel: ${service?["stewardessPhone"]}',
                    color: Colors.lightBlueAccent,
                  ),
                  _buildInfoCard(
                    icon: Icons.directions_car,
                    title: 'Servis Bilgileri',
                    description:
                    'Plaka: ${service?["plate"]}\nKapasite: ${service?["capacity"]}',
                    color: Colors.greenAccent,
                  ),
                  _buildInfoCard(
                    icon: Icons.access_time,
                    title: 'Kalkış Bilgileri',
                    description:
                    '${service?["departureLocation"]}\nSaat: ${service?["departureTime"]}',
                    color: Colors.pinkAccent,
                  ),
                ],
              ),
              const SizedBox(height: 80),
              Row(
                children: [
                  const SizedBox(width: 10),
                  const Text(
                    "Sürüşü Başlat",
                    style: TextStyle(
                      fontFamily: 'Poppins',
                      fontWeight: FontWeight.bold,
                      fontSize: 22,
                      color: Colors.black54,
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 10),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  _buildStyledButton(
                    "Okula Gidiş",
                    Colors.lightBlueAccent,
                    const BorderRadius.only(
                      topLeft: Radius.circular(20),
                      bottomLeft: Radius.circular(20),
                    ), () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(builder: (context) => const DrivingListPage(tripType: 0)),
                      );
                    },
                  ),
                  const SizedBox(width: 6),
                  _buildStyledButton(
                    "Okuldan Dönüş",
                    Colors.orangeAccent,
                    const BorderRadius.only(
                      topRight: Radius.circular(20),
                      bottomRight: Radius.circular(20),
                    ),
                        () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(builder: (context) => const DrivingListPage(tripType: 1)),
                      );
                    },
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: 0,
        onTap: (index) {
          if (index == 1) {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => ServiceStudentList(capacity: service?["capacity"],)),
            );
          } else if (index == 2) {
            /*Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => const MessagesScreen()),
            );*/
          }
        },
        type: BottomNavigationBarType.fixed,
        selectedItemColor: Colors.blueAccent,
        unselectedItemColor: Colors.grey,
        items: const [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Ana Sayfa'),
          BottomNavigationBarItem(icon: Icon(Icons.list), label: 'Öğrenci Listesi'),
          BottomNavigationBarItem(icon: Icon(Icons.notifications), label: 'Bildirimler'),
        ],
      ),
    );
  }

  Widget _buildStyledButton(
      String label, Color color, BorderRadius borderRadius, VoidCallback? onPressed) {
    return Expanded(
      child: ElevatedButton(
        style: ElevatedButton.styleFrom(
          backgroundColor: color,
          shape: RoundedRectangleBorder(
            borderRadius: borderRadius,
          ),
          padding: const EdgeInsets.symmetric(vertical: 25),
        ),
        onPressed: onPressed,
        child: Text(
          label,
          style: const TextStyle(
            fontFamily: 'Poppins',
            fontSize: 20,
            color: Colors.white,
          ),
        ),
      ),
    );
  }

  Widget _buildInfoCard({
    required IconData icon,
    required String title,
    required String description,
    required Color color,
  }) {
    return Container(
      decoration: BoxDecoration(
        color: color.withOpacity(0.2),
        borderRadius: BorderRadius.circular(15),
      ),
      padding: const EdgeInsets.all(16),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(icon, size: 50, color: color),
          const SizedBox(height: 10),
          Text(
            title,
            style: const TextStyle(
              fontFamily: 'Poppins',
              fontWeight: FontWeight.bold,
              fontSize: 16,
            ),
          ),
          const SizedBox(height: 10),
          Text(
            description,
            textAlign: TextAlign.center,
            style: const TextStyle(
              fontFamily: 'Poppins',
              color: Colors.black54,
              fontSize: 12,
            ),
          ),
        ],
      ),
    );
  }
}