# 2021-NCU-IOT-Final
Final project for IOT course

## 期末JSON格式

### linkit to server
* 按服務鈴
    * type: service_ring, order_ring, leave_ring
    * table_id: 桌號 int
### zenbo to server
* 帶位
    * type: find_table
    * customer_number: 人數 int
* 點餐
    * type: order
    * table_id: 桌號 int
    * 菜id int
*  儲存評價(暫定)
    *  type: recommend
    *  text: 內容 string
### server to zenbo
* 帶位空桌號
    * type: response_table
    * table_id: 桌號 int
    * x: 相對x double
    * y: 相對y double
* 通知zenbo服務(客人按服務鈴)
    * type: response_service
    * table_id: 桌號 int
    * x: 相對x double
    * y: 相對y double
* 通知zenbo服務(客人按離開鈴)
    * type: response_leave
    * table_id: 桌號 int
    * x: 相對x double
    * y: 相對y double
