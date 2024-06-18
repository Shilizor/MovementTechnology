namespace MovementTechnology.Data;

public class HistoryMovement
{
    public int Id { get; set; } // Индефикатор
    public int TechnicId { get; set; } // Какая техника перемещалась
    public int DepartamentId { get; set; } // Из какого отдела
    public int DepartamentCurrentId { get; set; } // В какой отдел
    public int StaffId { get; set; } // От какого сотрудника
    public int StaffCurrentId { get; set; } // К какому сотруднику
    public DateTime DateTime { get; set; } // Когда перемещалась
    public string UserId { get; set; } // Кто переместил
}