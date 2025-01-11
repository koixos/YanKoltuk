class StudentModel {
  String idNo;
  String name;
  String schoolNo;
  String plate;
  double? latitude;
  double? longitude;

  StudentModel({
    required this.idNo,
    required this.name,
    required this.schoolNo,
    required this.plate,
    required this.latitude,
    required this.longitude,
  });

  Map<String, dynamic> toJson() {
    return {
      'idNo': idNo,
      'name': name,
      'schoolNo': schoolNo,
      'plate': plate,
      'latitude': latitude,
      'longitude': longitude,
    };
  }

  factory StudentModel.fromJson(Map<String, dynamic> json) {
    return StudentModel(
      idNo: json['idNo'] ?? '',
      name: json['name'] ?? '',
      schoolNo: json['schoolNo'] ?? '',
      plate: json['plate'] ?? '',
      latitude: json['latitude'] as double?,
      longitude: json['longitude'] as double?,
    );
  }
}