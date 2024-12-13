class UpdateStudentModel {
  String plate;

  UpdateStudentModel({
    required this.plate,
  });

  Map<String, dynamic> toJson() {
    return {
      'plate': plate,
    };
  }

  factory UpdateStudentModel.fromJson(Map<String, dynamic> json) {
    return UpdateStudentModel(
      plate: json['plate'] ?? '',
    );
  }
}