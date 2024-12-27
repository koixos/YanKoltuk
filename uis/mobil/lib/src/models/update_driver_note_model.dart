class UpdateDriverNoteModel {
  String driverNote;

  UpdateDriverNoteModel({
    required this.driverNote,
  });

  Map<String, dynamic> toJson() {
    return {
      'driverNote': driverNote,
    };
  }
}