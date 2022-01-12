# Sequence Task system

一个简单的任务队列系统 由 [UniTask](https://github.com/Cysharp/UniTask) 实现等待机制，不准备扩充功能，之前由协程实现的感觉搞太多功能也没意义

> - [x] 按顺序执行多个任务
> - [x] 支持某个任务可以从内部中断后续任务的执行
> - [x] 支持外部终止任务队列执行 (终止时可选是否执行回调)
> - [x] 单元测试
> - [ ] 考虑一下 `Driver` 的对外接口 `Execute` 改成异步的是否更好