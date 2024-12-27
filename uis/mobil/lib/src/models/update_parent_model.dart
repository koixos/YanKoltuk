import 'package:flutter/cupertino.dart';

class UpdateParentModel {
  String phone;
  String address;

  UpdateParentModel ({
    required this.phone,
    required this.address
  });

  Map<String, dynamic> toJson() {
    return {
      'phone': phone,
      'address': address
    };
  }
}