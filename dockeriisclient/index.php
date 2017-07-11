<?php
$response = file_get_contents('http://dockerserver.test-application:5000/api/values');
var_dump($response);
?>