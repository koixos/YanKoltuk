import 'package:flutter/material.dart';
import 'package:mobil/src/models/excluded_dates_model.dart';
import 'package:table_calendar/table_calendar.dart';
import 'package:intl/intl.dart';

import '../service/parent_service.dart';

class CalendarPage extends StatefulWidget {
  final int? studentId;
  final String studentName;
  DateTime? excludedStartDate;
  DateTime? excludedEndDate;

  CalendarPage({
    super.key,
    required this.studentId,
    required this.studentName,
    required this.excludedStartDate,
    required this.excludedEndDate,
  });

  @override
  State<CalendarPage> createState() => _CalendarPageState();
}

class _CalendarPageState extends State<CalendarPage> {
  final ParentService _parentService = ParentService();

  final Set<DateTime> _markedDays = {};
  DateTime? _startDate;
  DateTime? _endDate;
  DateTime _focusedDay = DateTime.now();
  String _statusMessage = "Başlangıç tarihi seçiniz:";
  bool _showActionButtons = false;

  @override
  void initState() {
    super.initState();
    _fetchExcludedDates();
  }

  Future<void> _fetchExcludedDates() async {
    List<DateTime>? excludedDays = await _parentService.getExcludedDates(widget.studentId);
    setState(() {
      if (excludedDays != null) {
        _markedDays.addAll(excludedDays);
      }
    });
  }

  Future<void> _handleSubmit(DateTime? startDate, DateTime? endDate, int? studentId) async {
    final excludedDates = ExcludedDatesModel(
        startDate: startDate,
        endDate: endDate
    );
    final response = await _parentService.setExcludedDates(excludedDates, studentId!);
    if (response) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Başarıyla izin alındı.")),
      );

      Future.delayed(const Duration(seconds: 2), () {
        Navigator.pushNamed(context, '/parentDashboard');
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("İzin alınamadı. Lütfen tekrar deneyiniz.")),
      );
    }
  }

  void _resetSelection() {
    setState(() {
      _startDate = null;
      _endDate = null;
      _statusMessage = "Başlangıç tarihi seçiniz:";
      _showActionButtons = false;
    });
  }

  void _showFinalizeSelectionDialog(BuildContext context) {
    if (_startDate != null && _endDate != null) {
      showDialog(
        context: context,
        builder: (context) {
          final formattedStartDate = DateFormat('dd/MM/yyyy').format(_startDate!);
          final formattedEndDate = DateFormat('dd/MM/yyyy').format(_endDate!);
          return AlertDialog(
            title: Text("Onay"),
            content: Text(
                "Öğrenciniz ${widget.studentName}, $formattedStartDate ve $formattedEndDate tarihleri arasında izinli olarak işaretlenecektir. Onaylıyor musunuz?"),
            actions: [
              TextButton(
                onPressed: () {
                  Navigator.pop(context);
                },
                child: Text("Hayır"),
              ),
              TextButton(
                onPressed: () async {
                  Navigator.pop(context);
                  await _handleSubmit(_startDate!, _endDate!, widget.studentId);
                  setState(() {
                    DateTime currentDay = _startDate!;
                    while (!isSameDay(currentDay, _endDate!.add(Duration(days: 1)))) {
                      _markedDays.add(currentDay);
                      currentDay = currentDay.add(Duration(days: 1));
                    }
                    _resetSelection();
                  });
                },
                child: Text("Evet"),
              ),
            ],
          );
        },
      );
    }
  }

  void _showConfirmDayRemovalDialog(DateTime? startDate, DateTime? endDate) {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: Text("İzinleri Kaldır"),
          content: Text(
              "Bugünden itibaren tüm izinler kaldırılacaktır. Onaylıyor musunuz?"),
          actions: [
            TextButton(
              onPressed: () {
                Navigator.pop(context);
              },
              child: Text("Hayır"),
            ),
            TextButton(
              onPressed: () {
                _handleSubmit(startDate, endDate, widget.studentId);
                Navigator.pop(context);
                /*setState(() {
                  _markedDays.removeWhere((day) => !day.isBefore(selectedDay) || isSameDay(day, selectedDay));

                  if (_markedDays.isNotEmpty) {
                    widget.excludedStartDate = _markedDays.reduce((a, b) => a.isBefore(b) ? a : b);
                    widget.excludedEndDate = _markedDays.reduce((a, b) => a.isAfter(b) ? a : b);
                  } else {
                    widget.excludedStartDate = null;
                    widget.excludedEndDate = null;
                  }

                  _resetSelection();
                });*/
              },
              child: Text("Evet"),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("İzin Al"),
      ),
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          SizedBox(height: 10),
          TableCalendar(
              focusedDay: _focusedDay,
              firstDay: DateTime.utc(2000),
              lastDay: DateTime.utc(2100),
              selectedDayPredicate: (day) =>
                isSameDay(_startDate, day) || isSameDay(_endDate, day),
              calendarStyle: CalendarStyle(
                todayDecoration: BoxDecoration(
                  color: Colors.blue,
                  shape: BoxShape.circle,
                ),
                weekendTextStyle: TextStyle(color: Colors.red),
                defaultTextStyle: TextStyle(color: Colors.black),
              ),
              calendarBuilders: CalendarBuilders(
                defaultBuilder: (context, day, focusedDay) {
                  if (isSameDay(_startDate, day) || isSameDay(_endDate, day)) {
                    return Center(
                      child: Container(
                        width: 40,
                        height: 40,
                        decoration: BoxDecoration(
                          color: Colors.yellow,
                          shape: BoxShape.circle,
                        ),
                        child: Center(
                          child: Text(
                            '${day.day}',
                            style: TextStyle(color: Colors.black, fontWeight: FontWeight.bold),
                          ),
                        ),
                      ),
                    );
                  }
                  return null;
                },
                markerBuilder: (context, day, events) {
                  if (_markedDays.any((markedDay) => isSameDay(markedDay, day))) {
                    return Positioned(
                      bottom: 1,
                      child: Container(
                        width: 10,
                        height: 10,
                        decoration: BoxDecoration(
                          color: Colors.red,
                          shape: BoxShape.circle,
                        ),
                      ),
                    );
                  }
                  return null;
                },
              ),
              onDaySelected: (selectedDay, focusedDay) {
                setState(() {
                  _focusedDay = focusedDay;

                  if (widget.excludedStartDate != null && widget.excludedEndDate != null) {
                    final isStartDate = isSameDay(selectedDay, widget.excludedStartDate!);
                    final isEndDate = isSameDay(selectedDay, widget.excludedEndDate!);

                    if (isStartDate) {
                      _showConfirmDayRemovalDialog(null, null);
                      return;
                    }

                    if (selectedDay.isAfter(widget.excludedStartDate!) &&
                        (selectedDay.isBefore(widget.excludedEndDate!) || isEndDate)) {
                      _showConfirmDayRemovalDialog(widget.excludedStartDate, selectedDay.subtract(Duration(days: 1)));
                      return;
                    }

                    ScaffoldMessenger.of(context).showSnackBar(
                      SnackBar(
                        content: Text("Öğrencinizin seçilmiş izinli günlerin var. Yeni bir izin aralığı seçmek için önce mevcut izinleri iptal etmelisiniz."),
                      ),
                    );
                    return;
                  }

                  if (_startDate != null && _endDate == null) {
                    if (selectedDay.isBefore(_startDate!)) {
                      ScaffoldMessenger.of(context).showSnackBar(
                        SnackBar(
                          content: Text("İzin bitiş tarihi, iznin başlangıç tarihinden önce olamaz!"),
                        ),
                      );
                    } else {
                      _endDate = selectedDay;
                      _statusMessage = "Başlangıç ve bitiş tarihleri seçildi.";
                      _showActionButtons = true;
                    }
                  } else if (_startDate == null || (_startDate != null && _endDate != null)) {
                    _startDate = selectedDay;
                    _endDate = null;
                    _statusMessage = "Bitiş tarihi seçiniz:";
                    _showActionButtons = false;
                  }
                });
              }
          ),
          SizedBox(height: 50),
          if (_startDate == null && _endDate == null)
            Padding(
              padding: EdgeInsets.symmetric(horizontal: 16.0),
              child: Text(
                " Lütfen yukarıdaki takvimden öğrencinizin gelmeyeceği tarih aralığını seçiniz.\n"
                " Öğrenci sadece bir gün gelmeyecekse başlangıç ve bitiş tarihini aynı gün olarak seçiniz."
                " İzini iptal etmek için ise takvimdeki kırmızı ile gösterilen günlere tıklayabilirsiniz .\n",
                style: TextStyle(
                  fontSize: 17,
                  color: Colors.grey,
                  fontWeight: FontWeight.w500,
                ),
                textAlign: TextAlign.center,
              ),
            ),
          if (_startDate != null || _endDate != null)
            Center(
              child: Container(
                padding: EdgeInsets.all(10),
                margin: EdgeInsets.symmetric(horizontal: 16.0),
                decoration: BoxDecoration(
                  border: Border.all(color: Colors.grey.shade300),
                  borderRadius: BorderRadius.circular(10),
                  color: Colors.grey.shade100,
                ),
                child: Column(
                  children: [
                    GestureDetector(
                      onTap: () {
                        setState(() {
                          _statusMessage = "Başlangıç tarihi seçiniz:";
                          _startDate = null;
                        });
                      },
                      child: Text(
                        "Başlangıç Tarihi: ${_startDate != null ? DateFormat('dd/MM/yyyy').format(_startDate!) : 'Seçilmedi'}",
                        style: TextStyle(fontSize: 18, fontWeight: FontWeight.w500),
                      ),
                    ),
                    SizedBox(height: 5),
                    GestureDetector(
                      onTap: () {
                        setState(() {
                          _statusMessage = "Bitiş tarihi seçiniz:";
                          _endDate = null;
                        });
                      },
                      child: Text(
                        "Bitiş Tarihi: ${_endDate != null ? DateFormat('dd/MM/yyyy').format(_endDate!) : 'Seçilmedi'}",
                        style: TextStyle(fontSize: 18, fontWeight: FontWeight.w500),
                      ),
                    ),
                  ],
                ),
              ),
            ),
          Spacer(),
          if (_showActionButtons)
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                ElevatedButton.icon(
                  onPressed: _resetSelection,
                  icon: Icon(Icons.close),
                  label: Text("İptal"),
                  style: ElevatedButton.styleFrom(foregroundColor: Colors.white, backgroundColor: Colors.red),
                ),
                ElevatedButton.icon(
                  onPressed: () => _showFinalizeSelectionDialog(context),
                  icon: Icon(Icons.check),
                  label: Text("Onayla"),
                  style: ElevatedButton.styleFrom(foregroundColor: Colors.white, backgroundColor: Colors.green),
                ),
              ],
            ),
          SizedBox(height: 20),
        ],
      ),
    );
  }
}
