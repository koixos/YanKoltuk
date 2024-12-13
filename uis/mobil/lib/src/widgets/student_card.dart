import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobil/src/models/student_model.dart';
import 'package:mobil/src/models/student_service_model.dart';

import 'calendar_page.dart';

class StudentCard extends StatelessWidget {
  final StudentServiceModel student;
  final ValueChanged<bool> onStatusChanged;
  final VoidCallback onEdit;
  final VoidCallback onDelete;

  const StudentCard({
    super.key,
    required this.student,
    required this.onStatusChanged,
    required this.onEdit,
    required this.onDelete
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: const EdgeInsets.symmetric(vertical: 12, horizontal: 16),
      child: Padding(
        padding: const EdgeInsets.all(20),
        child: Column(
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        student.name,
                        style: const TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                      ),
                      const SizedBox(height: 5),
                      Row(
                        children: [
                          Icon(
                            Icons.circle,
                            color: student.status == 'İndi' ? Colors.green : Colors.red,
                            size: 10,
                          ),
                          const SizedBox(width: 5),
                          Text(
                            student.status,
                            style: const TextStyle(fontSize: 12, fontWeight: FontWeight.bold),
                          ),
                        ],
                      ),
                      const SizedBox(height: 10),
                      Text(
                        "Servis: ${student.plate}",
                        style: const TextStyle(fontSize: 14),
                      ),
                    ],
                  ),
                ),
                Column(
                  children: [
                    ElevatedButton(
                      onPressed: () async {
                        final selectedDate = await Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) => CalendarPage(studentName: student.name),
                          ),
                        );

                        if (selectedDate != null) {
                          print("Seçilen tarih: $selectedDate");
                        }
                      },
                      style: ElevatedButton.styleFrom(
                        backgroundColor: student.isComingTomorrow ? Colors.green : Colors.red,
                      ),
                      child: Text(
                        student.isComingTomorrow ? "Okula Gelecek" : "Okula Gelmeyecek",
                        style: const TextStyle(color: Colors.white),
                      ),
                    ),
                  ],
                ),
                PopupMenuButton<String>(
                  onSelected: (String value) {
                    if (value == "edit") {
                      onEdit();
                    } else if (value == "delete") {
                      onDelete();
                    }
                  },
                  itemBuilder: (BuildContext context) => [
                    const PopupMenuItem(
                      value: "edit",
                      child: Text("Düzenle"),
                    ),
                    const PopupMenuItem(
                      value: "delete",
                      child: Text("Sil"),
                    ),
                  ],
                  child: const Icon(Icons.more_vert),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}