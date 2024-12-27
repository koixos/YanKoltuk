class StudentServiceModel {
  final int? studentId;
  final String idNo;
  final String name;
  final String schoolNo;
  final String parentName;
  final String parentPhone;
  final String address;
  final String plate;
  final String? driverNote;
  final String? direction;
  final DateTime? excludedStartDate;
  final DateTime? excludedEndDate;
  int? sortIndex;
  String status;

  StudentServiceModel({
    required this.studentId,
    required this.idNo,
    required this.name,
    required this.schoolNo,
    required this.parentName,
    required this.parentPhone,
    required this.address,
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
      'parentName': parentName,
      'parentPhone': parentPhone,
      'address': address,
      'plate': plate,
      'status': status,
      'driverNote': driverNote,
      'sortIndex': sortIndex,
      'direction': direction,
      'excludedStartDate': excludedStartDate?.toIso8601String(),
      'excludedEndDate': excludedEndDate?.toIso8601String(),
    };
  }

  factory StudentServiceModel.fromJson(Map<String, dynamic> json) {
    return StudentServiceModel(
      studentId: json['studentId'] as int?,
      idNo: json['idNo'] ?? '',
      name: json['name'] ?? '',
      schoolNo: json['schoolNo'] ?? '',
      plate: json['plate'] ?? '',
      parentName: json['parentName'] ?? '',
      parentPhone: json['parentPhone'] ?? '',
      address: json['address'] ?? '',
      status: json['status'] ?? '',
      driverNote: json['driverNote'] ?? '',
      sortIndex: json['sortIndex'] as int?,
      direction: json['direction'] ?? '',
      excludedStartDate: json['excludedStartDate'] != null
        ? DateTime.parse(json['excludedStartDate'])
        : null,
      excludedEndDate: json['excludedEndDate'] != null
        ? DateTime.parse(json['excludedEndDate'])
        : null,
    );
  }
}