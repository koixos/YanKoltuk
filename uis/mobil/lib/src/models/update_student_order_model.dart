class UpdateStudentOrderModel {
  int? studentId;
  int? sortIndex;

  UpdateStudentOrderModel({
    required this.studentId,
    required this.sortIndex
  });

  Map<String, dynamic> toJson() {
    return {
      'studentId': studentId,
      'sortIndex': sortIndex
    };
  }
}