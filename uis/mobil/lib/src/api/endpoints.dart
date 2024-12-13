class Endpoints {
  static const signupParent = "/auth/signup";
  static const login = "/auth/login";
  static const getStudents = "/parent/students";
  static const getServicePlates = "/parent/plates";
  static const addStudent = "/parent/addStudent";
  static String editStudent(int id) => "/parent/updateStudent/$id";
  static String deleteStudent(int id) => "/parent/deleteStudent/$id";
  //static const getServiceIdByPlate = "/parent/service/${plate}";
}