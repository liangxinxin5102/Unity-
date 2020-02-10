<?php
//header("Content-Type: text/html;charset=utf-8");

$redis = new Redis();
$redis->connect("127.0.0.1", 6379);
// sort rankid by user.*->score get #
$result = $redis->sort("rankid", array("BY"=>"user.*->score", "SORT"=>"DESC", "LIMIT"=>array(0,20), "GET"=>array('#', "user.*->name", "user.*->score")));
//print_r($result);

$arr = array();
for( $i=0; $i<count($result); $i+=3)
{
    $id = $result[$i];
    $name = $result[$i+1];
    $score = $result[$i+2];
    $arr[$id]["id"] = $id;
    $arr[$id]["name"] = $name;
    $arr[$id]["score"] = $score;
}

echo json_encode($arr);