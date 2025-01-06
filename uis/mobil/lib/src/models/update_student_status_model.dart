class UpdateStudentStatusModel {
  final String status;
  final String direction;

  UpdateStudentStatusModel({
    required this.status,
    required this.direction
  });

  Map<String, dynamic> toJson() {
    return {
      'status': status,
      'direction': direction
    };
  }
}