import 'package:flutter/material.dart';
import 'package:mobil/src/models/student_service_model.dart';

class StudentCard extends StatelessWidget {
  final StudentServiceModel student;
  final VoidCallback onEdit;
  final VoidCallback onDelete;
  final VoidCallback onSetExcludedDates;

  const StudentCard({
    super.key,
    required this.student,
    required this.onEdit,
    required this.onDelete,
    required this.onSetExcludedDates,
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: const EdgeInsets.symmetric(vertical: 12, horizontal: 16),
      child: Padding(
        padding: const EdgeInsets.all(20),
        child: Row(
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
                  const SizedBox(height: 8),
                  Text(
                    "Servis: ${student.plate}",
                    style: TextStyle(fontSize: 14),
                  ),
                ],
              ),
            ),
            Row(
              children: [
                Container(
                  padding: EdgeInsets.symmetric(horizontal: 20, vertical: 10),
                  decoration: BoxDecoration(
                    color: student.status == 'İndi'
                        ? Colors.green.shade100
                        : Colors.red.shade100,
                    borderRadius: BorderRadius.circular(8),
                    border: Border.all(
                      color: student.status == 'İndi'
                          ? Colors.green
                          : Colors.red,
                    ),
                  ),
                  child: Row(
                    children: [
                      Icon(
                        Icons.circle,
                        color: student.status == 'İndi'
                            ? Colors.green
                            : Colors.red,
                        size: 10,
                      ),
                      SizedBox(width: 4),
                      Text(
                        student.status,
                        style: TextStyle(fontSize: 14, fontWeight: FontWeight.w700, color: Colors.black),
                      ),
                    ],
                  ),
                ),
                SizedBox(width: 16),
                PopupMenuButton<String>(
                  onSelected: (String value) {
                    if (value == "edit") {
                      onEdit();
                    } else if (value == "delete") {
                      onDelete();
                    } else if (value == "notComing") {
                      onSetExcludedDates();
                    }
                  },
                  itemBuilder: (BuildContext context) => [
                    PopupMenuItem(
                      value: "edit",
                      child: Text("Düzenle"),
                    ),
                    PopupMenuItem(
                      value: "delete",
                      child: Text("Sil"),
                    ),
                    PopupMenuItem(
                      value: "notComing",
                      child: Text("İzin Al"),
                    ),
                  ],
                  child: Icon(Icons.more_vert),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}