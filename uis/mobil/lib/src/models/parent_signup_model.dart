class ParentSignupModel {
  final String name;
  final String idNo;
  final String phone;
  final String address;
  final String passwd;

  ParentSignupModel({
    required this.name,
    required this.idNo,
    required this.phone,
    required this.address,
    required this.passwd
  });

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'idNo': idNo,
      'phone': phone,
      'address': address,
      'password': passwd
    };
  }
}