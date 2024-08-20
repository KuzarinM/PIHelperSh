# Библиотека конфигурации
Данная библиотека предназначена для упрощения процесса конфигурирования параметров (appsettings.json + env)
## Автоматическое конфигурирование
Если имеется некоторый объект, имеющий конфигурацию из нескольких элементов разумно будет объединить их в группу. Для этого создадим класс, содержащий нужные поля. Название класса должно совпадать с именем секции в appsettings.json (слово Configuration, если оно есть в имени класса можно опустить в названии секции), а имена переменных - с их наименованиями в этой секции.  Например для данного фрагмента appsettings.json
```json
{
	"TestConfiguration":{
	  "Test":"some text",
	  "Number": 123,
	  "AnotherTest": "another text"
	}
}
```
Можно создать такой конфигурационный класс:
```c#
[AutoConfiguration]
public class TestConfiguration
{
    public string Test { get; init; }
    public int Number { get; init; }
    [FromEnvironment("TEST_CONFIG")]
    public string AnotherTest { get; init; }
}
```
 * Атрибут **AutoConfiguration** указывает на то, что данный класс является классом конфигурации и будет добавлен и обработан автоматически. Он обязателен для выполнения автоматической конфигурации
 * Атрибут **FromEnvironment** указывает на то, что данное может быть заполнено из env с именем *TEST_CONFIG* при этом, если таковой нет, то значение будет взято из appsettings.json как и остальные
 * В случае отсутствия атрибута **FromEnvironment** возможность заполнения переменной из env остаётся. В таком случае, в качестве имени используется конструкция вида "*ИМЯ_СЕКЦИИ_ИМЯ_ПЕРЕМЕННОЙ*". Например, в случае *AnotherTest* это будет *ANOTHER_TEST*
Для непосредственного выполнения конфигурирования разом всех классов помеченных атрибутом *AutoConfiguration* необходимо использовать метод расширения библиотеки:
```c#
builder.Services.AddConfigurations(builder.Configuration);
```
Далее класс можно получить из DI контейнера через конструктор следующим образом:
```c#
public TestClass(IOptions<TestConfiguration> options)
{
  _config = options.Value
}
```
# Константы
Библиотека имеет возможность определять "константы" - статические переменные класса, заполняемые автоматически из appsettings.json и env. Для создания таковой необходимо, в первую очередь, отметить класс, в который она добавляется атрибутом **TrackedType**. Он позволит системе определить, что в данном классе в целом нужно искать "константы". Далее, как отмечалось ранее, переменные должны быть статическими. При этом это могут быть и свойства и поля. Так же необходимо наличие атрибута **Constant**. Пример класса с "константами":
``` c#
[TrackedType]
public class TestClass
{
   [Constant(BlockName = "Test", ConstantName = "Test1")]
   static int _test1;

   [Constant(BlockName = "Test", ConstantName = "Test2")]
   static TestEnum _testEnum;

   [Constant(BlockName = "Test")]
   static string _test3 { get; set; } = null!;

   [Constant(BlockName = "Test", ConstantName = "Test3")]
   static string _test5 = null!;

   [Constant(BlockName = "Test", VariableName = "ANOTHER_NAME")]
   public static string Test4 = null!;
 
   [Constant(BlockName = "Test", ConstantName = "Test4")]
   public static string Test6 { get; set; } = null!;
   ...
}
 ```
 Вид фрагмента appsettings.json для описанного ранее класса:
```json
{
	"Test": {
	  "Test1": 123,
	  "Test2": 3,
	  "Test3": "abacaba",
	  "Test4": "hello world"
	}
}
```
 * Атрибут Constant может получать значения из appsettings.json и из env (приоритет). Поддерживается вариант, при котором указывается только имя секции (**BlockName** ), в которой находится переменная, в то время как имя берётся из имени "константы" (символ '_' использовать в именовании переменной можно, он будет игнорироваться). Так же имя можно задать самостоятельно через свойство **ConstantName** . Важно отметить, что как и в случае с конфигурацией, если не указать имя env (через свойство **VariableName**) , оно будет взято как "*ИМЯ_СЕКЦИИ_ИМЯ_ПЕРЕМЕННОЙ*".
 Для того, чтобы заполнить все "константы" значениями необходимо воспользоваться методом расширения **AddConstants**.
```c#
builder.Configuration.AddConstants();
```
## Ручное конфигурирование
В случае необходимости, библиотека поддерживает возможность ручного конфигурирования. Оно полностью аналогично автоматическому, с той лишь разницей, что атрибут **AutoConfiguration**  не требуется, а классы, которые будут использоваться как конфигурационные необходимо зарегистрировать индивидуально, используя метод расширения **ConfigureWithENV**
```c#
builder.Services.ConfigureWithENV<TestConfiguration>(builder.Configuration)
```

