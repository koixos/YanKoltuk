import 'package:flutter/material.dart';

class ServiceTrackingPage extends StatefulWidget {
  @override
  _ServiceTrackingPageState createState() => _ServiceTrackingPageState();
}

class _ServiceTrackingPageState extends State<ServiceTrackingPage> {
  // Başlangıç öğrenci listesi
  List<Student> students = [
    Student(name: "Ahmet Yılmaz", plateNumber: "34ABC123", status: "indi", studentNumber: "12345", tcNumber: "11111111111"),
    Student(name: "Elif Demir", plateNumber: "34XYZ789", status: "bindi", studentNumber: "12346", tcNumber: "22222222222"),
    Student(name: "Ayşe Kaya", plateNumber: "34KLM456", status: "indi", studentNumber: "12347", tcNumber: "33333333333"),
    Student(name: "Mehmet Öz", plateNumber: "34ABC123", status: "bindi", studentNumber: "12348", tcNumber: "44444444444"),
  ];

  List<String> servicePlates = ["34ABC123", "34XYZ789", "34KLM456"];
  String selectedPlate = '34ABC123';

  void _addStudent() {
    String name = '';
    String studentNumber = '';
    String tcNumber = '';

    showDialog(
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (context, setDialogState) {
            return AlertDialog(
              title: Text("Öğrenci Ekle"),
              content: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  TextField(
                    decoration: InputDecoration(labelText: "İsim Soyisim"),
                    onChanged: (value) => name = value,
                  ),
                  TextField(
                    decoration: InputDecoration(labelText: "Öğrenci Numarası"),
                    onChanged: (value) => studentNumber = value,
                  ),
                  TextField(
                    decoration: InputDecoration(labelText: "TC Kimlik Numarası"),
                    onChanged: (value) => tcNumber = value,
                  ),
                  SizedBox(height: 10),
                  Text("Servis Plakasını Seçin:"),
                  DropdownButton<String>(
                    value: selectedPlate,
                    onChanged: (String? newValue) {
                      setDialogState(() {
                        selectedPlate = newValue!;
                      });
                    },
                    items: servicePlates.map<DropdownMenuItem<String>>((String plate) {
                      return DropdownMenuItem<String>(
                        value: plate,
                        child: Text(plate),
                      );
                    }).toList(),
                  ),
                  SizedBox(height: 10),
                  Text("Seçilen Plaka: $selectedPlate"),
                ],
              ),
              actions: [
                TextButton(
                  onPressed: () {
                    if (name.isNotEmpty && studentNumber.isNotEmpty && tcNumber.isNotEmpty) {
                      setState(() {
                        students.add(Student(
                          name: name,
                          plateNumber: selectedPlate,
                          status: "indi",
                          studentNumber: studentNumber,
                          tcNumber: tcNumber,
                        ));
                      });
                      Navigator.pop(context);
                    }
                  },
                  child: Text("Ekle"),
                ),
              ],
            );
          },
        );
      },
    );
  }

  void _editStudent(Student student) {
    String name = student.name;
    String studentNumber = student.studentNumber;
    String tcNumber = student.tcNumber;
    String selectedEditPlate = student.plateNumber;

    showDialog(
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (context, setDialogState) {
            return AlertDialog(
              title: Text("Öğrenci Düzenle"),
              content: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  TextField(
                    decoration: InputDecoration(labelText: "İsim Soyisim"),
                    controller: TextEditingController(text: name),
                    onChanged: (value) => name = value,
                  ),
                  TextField(
                    decoration: InputDecoration(labelText: "Öğrenci Numarası"),
                    controller: TextEditingController(text: studentNumber),
                    onChanged: (value) => studentNumber = value,
                  ),
                  TextField(
                    decoration: InputDecoration(labelText: "TC Kimlik Numarası"),
                    controller: TextEditingController(text: tcNumber),
                    onChanged: (value) => tcNumber = value,
                  ),
                  SizedBox(height: 10),
                  Text("Servis Plakasını Seçin:"),
                  DropdownButton<String>(
                    value: selectedEditPlate,
                    onChanged: (String? newValue) {
                      setDialogState(() {
                        selectedEditPlate = newValue!;
                      });
                    },
                    items: servicePlates.map<DropdownMenuItem<String>>((String plate) {
                      return DropdownMenuItem<String>(
                        value: plate,
                        child: Text(plate),
                      );
                    }).toList(),
                  ),
                ],
              ),
              actions: [
                TextButton(
                  onPressed: () {
                    setState(() {
                      student.name = name;
                      student.studentNumber = studentNumber;
                      student.tcNumber = tcNumber;
                      student.plateNumber = selectedEditPlate;
                    });
                    Navigator.pop(context);
                  },
                  child: Text("Kaydet"),
                ),
              ],
            );
          },
        );
      },
    );
  }

  void _deleteStudent(Student student) {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: Text("Öğrenci Sil"),
          content: Text("${student.name} isimli öğrenci silinecek. Onaylıyor musunuz?"),
          actions: [
            TextButton(
              onPressed: () {
                setState(() {
                  students.remove(student);
                });
                Navigator.pop(context);
              },
              child: Text("Evet"),
            ),
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: Text("Hayır"),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Servis Takip")),
      body: students.isEmpty
          ? Center(
        child: Text(
          "Öğrenci eklemek için sağ altta bulunan + tuşuna basınız",
          style: TextStyle(color: Colors.grey, fontSize: 20),
          textAlign: TextAlign.center,
        ),
      )
          : ListView.builder(
        itemCount: students.length,
        itemBuilder: (context, index) {
          return StudentCard(
            student: students[index],
            onStatusChanged: (value) {
              setState(() {
                students[index].isComingTomorrow = value;
              });
            },
            onEdit: () => _editStudent(students[index]),
            onDelete: () => _deleteStudent(students[index]),
          );
        },
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _addStudent,
        child: Icon(Icons.add),
      ),
    );
  }
}

class Student {
  String name;
  String plateNumber;
  final String status; // 'indi' veya 'bindi'
  bool isComingTomorrow;
  String studentNumber;
  String tcNumber;

  Student({
    required this.name,
    required this.plateNumber,
    required this.status,
    required this.studentNumber,
    required this.tcNumber,
    this.isComingTomorrow = true,
  });
}

class StudentCard extends StatelessWidget {
  final Student student;
  final ValueChanged<bool> onStatusChanged;
  final VoidCallback onEdit;
  final VoidCallback onDelete;

  StudentCard({required this.student, required this.onStatusChanged, required this.onEdit, required this.onDelete});

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: EdgeInsets.symmetric(vertical: 8, horizontal: 16),
      child: Padding(
        padding: EdgeInsets.all(16),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Row(
                  children: [
                    Text(student.name, style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
                    SizedBox(width: 10),
                    Row(
                      children: [
                        Icon(
                          Icons.circle,
                          color: student.status == 'indi' ? Colors.green : Colors.red,
                          size: 10,
                        ),
                        SizedBox(width: 5),
                        Text(student.status, style: TextStyle(fontSize: 12, fontWeight: FontWeight.bold)),
                      ],
                    ),
                  ],
                ),
                Text("Servis: ${student.plateNumber}", style: TextStyle(fontSize: 14)),
              ],
            ),
            Column(
              children: [
                Text("Okula gelecek mi?", style: TextStyle(fontSize: 12)),
                Switch(
                  value: student.isComingTomorrow,
                  onChanged: onStatusChanged,
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
                    PopupMenuItem(
                      value: "edit",
                      child: Text("Öğrenci Düzenle"),
                    ),
                    PopupMenuItem(
                      value: "delete",
                      child: Text("Öğrenci Sil"),
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
