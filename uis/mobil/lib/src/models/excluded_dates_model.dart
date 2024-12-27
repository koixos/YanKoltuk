class ExcludedDatesModel {
  final DateTime? startDate;
  final DateTime? endDate;

  ExcludedDatesModel({
    required this.startDate,
    required this.endDate,
  });

  Map<String, dynamic> toJson() {
    return {
      'startDate': startDate?.toIso8601String(),
      'endDate': endDate?.toIso8601String(),
    };
  }

  factory ExcludedDatesModel.fromJson(Map<String, dynamic> json) {
    return ExcludedDatesModel(
      startDate: json['startDate'] != null ? DateTime.parse(json['startDate']) : null,
      endDate: json['endDate'] != null ?DateTime.parse(json['endDate']) : null
    );
  }
}