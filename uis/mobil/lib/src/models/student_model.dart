class StudentModel {
  String idNo;
  String name;
  String schoolNo;
  String plate;

  StudentModel({
    required this.idNo,
    required this.name,
    required this.schoolNo,
    required this.plate,
  });

  Map<String, dynamic> toJson() {
    return {
      'idNo': idNo,
      'name': name,
      'schoolNo': schoolNo,
      'plate': plate,
    };
  }

  factory StudentModel.fromJson(Map<String, dynamic> json) {
    return StudentModel(
      idNo: json['idNo'] ?? '',
      name: json['name'] ?? '',
      schoolNo: json['schoolNo'] ?? '',
      plate: json['plate'] ?? '',
    );
  }
}