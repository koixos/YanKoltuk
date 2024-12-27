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
}