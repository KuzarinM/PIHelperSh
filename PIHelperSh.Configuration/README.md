# ���������� ������������
������ ���������� ������������� ��� ��������� �������� ���������������� ���������� (appsettings.json + env)
## �������������� ����������������
���� ������� ��������� ������, ������� ������������ �� ���������� ��������� ������� ����� ���������� �� � ������. ��� ����� �������� �����, ���������� ������ ����. �������� ������ ������ ��������� � ������ ������ � appsettings.json (����� Configuration, ���� ��� ���� � ����� ������ ����� �������� � �������� ������), � ����� ���������� - � �� �������������� � ���� ������.  �������� ��� ������� ��������� appsettings.json
```json
...
"TestConfiguration":{
  "Test":"some text",
  "Number": 123,
  "AnotherTest": "another text"
}
...
```
����� ������� ����� ���������������� �����:
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
 * ������� **AutoConfiguration** ��������� �� ��, ��� ������ ����� �������� ������� ������������ � ����� �������� � ��������� �������������. �� ���������� ��� ���������� �������������� ������������
 * ������� **FromEnvironment** ��������� �� ��, ��� ������ ����� ���� ��������� �� env � ������ *TEST_CONFIG* ��� ����, ���� ������� ���, �� �������� ����� ����� �� appsettings.json ��� � ���������
 * � ������ ���������� �������� **FromEnvironment** ����������� ���������� ���������� �� env �������. � ����� ������, � �������� ����� ������������ ����������� ���� "*���_������_���_����������*". ��������, � ������ *AnotherTest* ��� ����� *ANOTHER_TEST*
��� ����������������� ���������� ���������������� ����� ���� ������� ���������� ��������� *AutoConfiguration* ���������� ������������ ����� ���������� ����������:
```c#
builder.Services.AddConfigurations(builder.Configuration);
```
����� ����� ����� �������� �� DI ���������� ����� ����������� ��������� �������:
```c#
public TestClass(IOptions<TestConfiguration> options)
{
  _config = options.Value
}
```
# ���������
���������� ����� ����������� ���������� "���������" - ����������� ���������� ������, ����������� ������������� �� appsettings.json � env. ��� �������� ������� ����������, � ������ �������, �������� �����, � ������� ��� ����������� ��������� **TrackedType**. �� �������� ������� ����������, ��� � ������ ������ � ����� ����� ������ "���������". �����, ��� ���������� �����, ���������� ������ ���� ������������. ��� ���� ��� ����� ���� � �������� � ����. ��� �� ���������� ������� �������� **Constant**. ������ ������ � "�����������":
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
 ��� ��������� appsettings.json ��� ���������� ����� ������:
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
 * ������� Constant ����� �������� �������� �� appsettings.json � �� env (���������). �������������� �������, ��� ������� ����������� ������ ��� ������ (**BlockName** ), � ������� ��������� ����������, � �� ����� ��� ��� ������ �� ����� "���������" (������ '_' ������������ � ���������� ���������� �����, �� ����� ��������������). ��� �� ��� ����� ������ �������������� ����� �������� **ConstantName** . ����� ��������, ��� ��� � � ������ � �������������, ���� �� ������� ��� env (����� �������� **VariableName**) , ��� ����� ����� ��� "*���_������_���_����������*".
 ��� ����, ����� ��������� ��� "���������" ���������� ���������� ��������������� ������� ���������� **AddConstants**.
```c#
builder.Configuration.AddConstants();
```
## ������ ����������������
� ������ �������������, ���������� ������������ ����������� ������� ����������������. ��� ��������� ���������� ���������������, � ��� ���� ��������, ��� ������� **AutoConfiguration**  �� ���������, � ������, ������� ����� �������������� ��� ���������������� ���������� ���������������� �������������, ��������� ����� ���������� **ConfigureWithENV**
```c#
builder.Services.ConfigureWithENV<TestConfiguration>(builder.Configuration)
```

