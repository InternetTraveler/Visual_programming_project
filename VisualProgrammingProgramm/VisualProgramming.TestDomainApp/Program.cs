using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;

namespace VisualProgramming.ConsoleTest;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Тестирование VisualProgramming.Domain ===\n");

        try
        {
            // Создаем проект
            var project = new Project("Test Project");
            Console.WriteLine($"[+] Создан проект: {project.Name.Value}");

            // Создаем граф
            var graf = new Graf(project);
            project.AddGraf(graf);
            Console.WriteLine($"[+] Создан граф: {graf.Id}");

            // Создаем узлы
            var node1 = new Node("Add Node", TypeOperation.ADD);
            var node2 = new Node("Multiply Node", TypeOperation.MUL);
            Console.WriteLine($"[+] Созданы узлы: {node1.Name.Value}, {node2.Name.Value}");

            // Создаем порты
            var port1 = new Port(node1, TypePort.INPUT, "Input port for addition");
            var port2 = new Port(node2, TypePort.OUTPUT, "Output port for multiplication");
            Console.WriteLine($"[+] Созданы порты");

            // Создаем связи узлов с портами
            var npc1 = new NodePortConnection(node1, port1);
            var npc2 = new NodePortConnection(node2, port2);

            var npc3 = new NodePortConnection(node1, port2);
            var npc4 = new NodePortConnection(node2, port1);
            Console.WriteLine($"[+] Созданы связи Node-Port");

            // Создаем элементы графа
            var element1 = new ElementGraf(node1, 1, false, graf, 100, 100);
            var element2 = new ElementGraf(node2, 1, false, graf, 300, 100);
            var element3 = new ElementGraf(node1, 1, false, graf, 100, 300);
            graf.AddElement(element1);
            graf.AddElement(element2);
            graf.AddElement(element3);
            Console.WriteLine($"[+] Добавлены элементы в граф");

            // Создаем соединение между портами
            var connection = new Connection(port1, port2, element1, element2);
            Console.WriteLine($"[+] Создано соединение между портами");

            // Тестируем обновления
            element1.UpdatePosition(150, 150);
            Console.WriteLine($"[+] Обновлена позиция элемента");

            connection.UpdateOutConnection(element3, port2);
            Console.WriteLine($"[+] Обновлено соединение");

            // Создаем модуль
            var moduleGraf = new Graf(project);
            var module = new Modul("Math Module", moduleGraf);
            Console.WriteLine($"[+] Создан модуль: {module.Name.Value}");

            // Выводим информацию
            Console.WriteLine("\n=== Итоговая информация ===");
            Console.WriteLine($"Элементов в графе: {graf.ElementsGraf.Count}");
            Console.WriteLine("По хорошу все не соеденёные элементы должны быть отдельным графом");

            Console.WriteLine("\nВсе тесты пройдены успешно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nОшибка: {ex.Message}");
            if (ex.InnerException != null)
                Console.WriteLine($"  Внутренняя ошибка: {ex.InnerException.Message}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}