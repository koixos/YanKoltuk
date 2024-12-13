class StudentServiceModel {
  int studentId;
  String idNo;
  String name;
  String schoolNo;
  String plate;
  String status;
  String? driverNote;
  int sortIndex;
  String? direction;
  DateTime? excludedStartDate;
  DateTime? excludedEndDate;

  StudentServiceModel({
    required this.studentId,
    required this.idNo,
    required this.name,
    required this.schoolNo,
    required this.plate,
    required this.status,
    required this.driverNote,
    required this.sortIndex,
    required this.direction,
    required this.excludedStartDate,
    required this.excludedEndDate,
  });

  Map<String, dynamic> toJson() {
    return {
      'studentId': studentId,
      'idNo': idNo,
      'name': name,
      'schoolNo': schoolNo,
      'plate': plate,
      'status': status,
      'driverNote': driverNote,
      'sortIndex': sortIndex,
      'direction': direction,
      'excludedStartDate': excludedStartDate,
      'excludedEndDate': excludedEndDate,
    };
  }

  factory StudentServiceModel.fromJson(Map<String, dynamic> json) {
    return StudentServiceModel(
      studentId: json['studentId'],
      idNo: json['idNo'] ?? '',
      name: json['name'] ?? '',
      schoolNo: json['schoolNo'] ?? '',
      plate: json['plate'] ?? '',
      status: json['status'] ?? '',
      driverNote: json['driverNote'] ?? '',
      sortIndex: json['sortIndex'],
      direction: json['direction'] ?? '',
      excludedStartDate: json['excludedStartDate'],
      excludedEndDate: json['excludedEndDate'],
    );
  }
}