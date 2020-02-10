<?php
//header("Content-Type: text/html;charset=utf-8");

// 连接数据库, 输入地址，用户名，密码和数据库名称
$myData=mysqli_connect( "localhost" ,"root" ,"123456", "myscoresdb" );
if ( mysqli_connect_errno()) // 如果连接数据库失败
{
    echo mysqli_connect_error();
    die();
    exit(0);
}

// 确保数据库文本使用UTF-8格式
mysqli_query($myData,"set names utf8");

// 查询
$requestSQL = "SELECT * FROM hiscores";

$result = mysqli_query($myData,$requestSQL) or die("SQL ERROR : ".$requestSQL);
$num_results = mysqli_num_rows($result);

// 前面的代码主要是连接MySQL数据库, 与downloadscores.php的代码基本相同
$arr =array();

$redis = new Redis();
$redis->connect("127.0.0.1", 6379);  // 连接到Redis
$redis->flushDB();  // 清空Redis
//
// 将查询结果写入到JSON格式的数组中
for($i = 0; $i < $num_results; $i++)
{
    $row = mysqli_fetch_array($result ,MYSQLI_ASSOC); // 获得一行数据

    $id=$row['id'];  // 获得ID
    $name=$row['name']; // 获得用户名
    $score=$row['score'];  // 获得分数
    
    // 将用户ID写入Redis数组（用于排序）
    $redis->lPush("rankid", $id);
    // 将用户数据写入Redis,键名是user.和ID编号的结合
    $redis->hMset("user.$id", array("id"=>$id, "name"=>$name, "score"=>$score) );
    
    echo "<p>redis " .$redis->hget("user.$id", "id") .".". $redis->hget("user.$id", "name") .":". $redis->hget("user.$id", "score"). "</p>"; // 查看是否正确写入Redis

}

mysqli_free_result($result);  // 释放SQL查询结果
mysqli_close($myData);  // 关闭数据库

?>


