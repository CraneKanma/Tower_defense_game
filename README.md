# Unity Tower Defense Game

一个使用Unity和C#开发的2D塔防游戏Demo。

## 功能特点
- 三种差异化的防御塔（普通塔/AOE范围塔/减速塔），各自带专属攻击特效
- 波次自动生成系统，敌人按配置的强度递增出现
- 多关卡系统，通关后自动解锁下一关
- 完整的UI交互（血量/金币显示、选塔面板、结算界面）
- 使用对象池优化敌人生成性能，减少GC开销
- ScriptableObject数据驱动设计，方便策划配置塔/敌人/波次数据

## 技术栈
- Unity 2D
- C#
- ScriptableObject 数据驱动架构
- 单例模式管理游戏状态

## 在线试玩
[点击这里在线试玩](https://cranekanma.github.io/Tower_defense_game/)

## 下载
[Windows版本下载链接]（如果打包了exe压缩包，可以放在GitHub Release里）
