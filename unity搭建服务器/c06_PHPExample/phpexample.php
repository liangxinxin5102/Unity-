<?php   //注释使用双斜杠，与C#一样, php的代码必须由<?php开始
$name = "Jack";  // 声明一个字符串变量，变量名开头必须用$，语句最后用;号结束
$number = 3 * 2 + (4 / 2);  // 基本的数学运算
if ($number>100){ // 判断语句
    echo "<p>$number 大于 100</p>"; // echo 是最常用输出函数，可以直接生成HTML文本
}
else{
    echo "<p>$number 小于 100</p>";  // 带有$符号的变量可以直接写入使用""号字符串中
}
for ($i=0; $i<5; $i++){  // for 循环语句
    echo '<p>number:'.$i.'</p>';  // 连接字符串使用 .
}
while( $number>0 )  // while循环
{
    echo "<p>while $number </p>"; 
    $number--;  // 自减
}
function functionname( $varname )  // 定义一个函数
{
    echo "<p> $varname </P>";
}
functionname("hello,world");  // 执行函数

$array = array("linux","mac","windows");  // 建立数组
//$array = ["linux","mac","windows"];  // 另一种建立数组的方式
$array[3] = "android";  //添加更多元素
array_push($array, "iphone");  //添加更多元素

print_r($array);  //打印数组
echo '<br>';  // HTML 换行符
foreach ($array as &$item) // foreach遍历数组
{
    echo "<p>$item</p>";
}

$arr = array("name"=> "android", "resulotion"=>"1024", "price"=>1000);  // 创建字典数组
$arr["vendor"] = "huawei"; // 添加更多字典元素
foreach( $arr as $key => $value)  // foreach遍历数组，打印出每项的键和值
{
    echo "<p> $key : $value </p>";
}

class People  // 定义一个类
{
    public $name = "Jack";  // 公有成员
    protected $money = 18;  // 保护成员
    private $age = 0;  // 私有成员
    function __construct($age)  // 使用关键字__construct创建类的构造函数
    {
        $this->age = $age;  // 通过赋值初始化私有成员
        echo "<p>new People</P>";
    }
    function Say($something)  // 类的成员函数
    {
        echo "<p> $this->name: $something.</p>";
    }
}

$p = new People(18);  // 创建一个对象，自动调用构造函数
$p->name = "David";  // 修改公有成员变量
$p->Say("Hello, How ar you?");  // 调用成员函数
?>
