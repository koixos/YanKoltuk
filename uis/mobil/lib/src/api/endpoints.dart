class Endpoints {
  static const signupParent = "/auth/signup";
  static const login = "/auth/login";
  static const getParentInfo = "/parent/info";
  static const getStudents = "/parent/students";
  static const getServicePlates = "/parent/plates";
  static const updateParent = "/parent/updateParent";
  static const addStudent = "/parent/addStudent";
  static String getExcludedDates(int id) => "/parent/excludedDates/$id";
  static String editStudent(int id) => "/parent/updateStudent/$id";
  static String deleteStudent(int id) => "/parent/deleteStudent/$id";
  static String setExcludedDates(int id) => "/parent/setExcludedDates/$id";

  static const getServiceInfo = "/service/info";
  static const getStudentsService = "/service/students";
  static const getDrivingList = "/service/drivingList";
  static const updateStudentOrder = "/service/editOrder";
  static String editNote(int id) => "/service/editNote/$id";
  static String updateStudentStatus(int id) => "/service/updateStatus/$id";

}