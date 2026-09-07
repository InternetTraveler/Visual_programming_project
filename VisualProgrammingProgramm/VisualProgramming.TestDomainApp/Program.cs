using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Tests;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("=== НАЧАЛО ТЕСТИРОВАНИЯ ===\n");

            // 1. Создание проекта
            var projectName = new Name("Тестовый проект");
            var project = new Project(projectName);
            Console.WriteLine($"✅ Создан проект: {project.Name.Value}");

            // 2. Создание графа
            var graf = new Graf(project);
            project.AddGraf(graf);
            Console.WriteLine($"✅ Создан граф, добавлен в проект");

            // 3. Создание узла (Node)
            var nodeName = new Name("Калькулятор");
            var node = new Node(nodeName, TypeOperation.ADD);
            Console.WriteLine($"✅ Создан узел: {node.Name.Value}, операция: {node.TypeOperation}");

            // 4. Создание модуля (Modul)
            var innerGraf = new Graf(project);
            var modulName = new Name("Внутренний модуль");
            var modul = new Modul(modulName, innerGraf);
            Console.WriteLine($"✅ Создан модуль: {modul.Name.Value}");

            // 5. Создание портов
            var inputPort = new Port(node, TypePort.INPUT, "Входной порт");
            var outputPort = new Port(node, TypePort.OUTPUT, "Выходной порт");
            var modulPort = new Port(modul, TypePort.INPUT, "Порт модуля");
            Console.WriteLine($"✅ Созданы порты: Input, Output, ModulPort");

            // 6. Связь портов с узлами через NodePortConnection
            var nodePortConn1 = new NodePortConnection(node, inputPort);
            var nodePortConn2 = new NodePortConnection(node, outputPort);
            var nodePortConn3 = new NodePortConnection(modul, modulPort);
            Console.WriteLine($"✅ Созданы связи Node-Port (3 шт.)");

            // 7. Создание элементов графа (ElementGraf)
            var level = 1;
            var elementNode = new ElementGraf(node, level, false, graf, 100, 200);
            var elementModul = new ElementGraf(modul, level, true, graf, 300, 400);

            graf.AddElement(elementNode);
            graf.AddElement(elementModul);
            Console.WriteLine($"✅ Элементы графа добавлены (Node и Modul)");

            // 8. Создание соединения между портами (Connection)
            var connection = new Connection(outputPort, inputPort, elementNode, elementModul);

            // Добавление соединения в элементы графа
            elementNode.AddConnection(connection);
            elementModul.AddConnection(connection);
            Console.WriteLine($"✅ Создано соединение между {outputPort.TypePort} -> {inputPort.TypePort}");

            // 9. Проверка связей
            Console.WriteLine("\n=== ПРОВЕРКА СВЯЗЕЙ ===");
            Console.WriteLine($"Проект содержит графов: {project.Grafs.Count}");
            Console.WriteLine($"Граф содержит элементов: {graf.ElementsGraf.Count}");
            Console.WriteLine($"Узел имеет портов через связи: {node.NodePortConnections.Count}");
            Console.WriteLine($"Порт входа имеет связей: {inputPort.NodePortConnections.Count}");
            Console.WriteLine($"Элемент-узел имеет соединений: {elementNode.ElementGrafConnections.Count}");
            Console.WriteLine($"Соединение связывает: {connection.InElementGraf.Node?.Name.Value} -> {connection.OutElementGraf.Node?.Name.Value}");

            // 10. Тестирование обновлений
            Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ОБНОВЛЕНИЙ ===");

            var newPosition = elementNode.UpdatePosition(500, 600);
            Console.WriteLine($"Обновление позиции: {(newPosition ? "OK" : "FAIL")}");

            var newLevel = elementNode.UpdateLevelOfDepth(2);
            Console.WriteLine($"Обновление уровня глубины: {(newLevel ? "OK" : "FAIL")}");

            var updated = connection.UpdateOutConnection(elementNode, inputPort);
            Console.WriteLine($"Обновление соединения: {(updated ? "OK" : "FAIL")}");

            // 11. Тестирование удаления
            Console.WriteLine("\n=== ТЕСТИРОВАНИЕ УДАЛЕНИЯ ===");

            elementNode.RemuveConnection(connection);
            Console.WriteLine($"Удаление соединения из элемента: OK");

            graf.RemoveElement(elementModul);
            Console.WriteLine($"Удаление элемента из графа: OK");

            project.RemoveGraf(graf);
            Console.WriteLine($"Удаление графа из проекта: OK");

            Console.WriteLine("\n=== ВСЕ ТЕСТЫ ПРОЙДЕНЫ УСПЕШНО ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ ОШИБКА: {ex.Message}");
            if (ex.InnerException != null)
                Console.WriteLine($"Внутренняя ошибка: {ex.InnerException.Message}");
        }
    }
}