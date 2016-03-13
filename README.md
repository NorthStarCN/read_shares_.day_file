# read_shares_.day_file
2016-03-06 C#实现 读取股票的.day文件				

用C#读取二进制文件

    string filePath = @"文件路径";
    using (FileStream fileStream = File.Open(filePath, FileMode.Open))
	{
		byte[] bytes = new byte[fileStream.Length];
	}

这样就把二进制文件所有字节都都出来了

然后来看看“.day”文件的二进制数据 这是按40个字节一换行输出的
![40字节换行原始数据](http://ww3.sinaimg.cn/large/4ff76fc7jw1f1vcyarx60j20vz0f6jxo.jpg)

可以看到第一天的20111010日期看起来还像是对的

但是后面几天就错乱了

之后这几天的日期是对的20111017 20111024 20111031

仔细看了数据之后发现这个文件应该是32个字节一天 每天结束都是0 0 1 0

调整到32字节一天之后
![32字节换行原始数据](http://ww1.sinaimg.cn/large/4ff76fc7jw1f1vcy6a9dxj20pg0ien41.jpg)

嗯 这样看来树蕨就差不多了

然后openPrice(开盘价) highestPrice(最高价) lowestPrice(最低价) closePrice(收盘价) money(成交额) volume(成交量)

这几个数根据网上的资料 每四个字节一个数 前四个数除100是RMB价格 成交额有问题 成交量好像也要除100因为什么100手算一个什么的

最后再说一下这个数怎么算 其实也很简单 拿最开始的四个数举例子

162 222 50 1 这是四个十进制数 要将他们先转成16进制数A2 DE 32 01

然后将这四个16进制数倒过来 就是01 32 DE A2 将他们看成一个数字0132DEA2 就是20111010了


最后再补两张世纪数据的对比图
![](http://ww3.sinaimg.cn/large/4ff76fc7jw1f1vcyh55z7j20qo0zk44a.jpg)
![](http://ww4.sinaimg.cn/large/4ff76fc7jw1f1vcyqpqklj20qo0zkafu.jpg)



--------------------

现在就是读取成交额有问题 不太对

网上大多都说40字节是一天的 但是对于这几个数据我发现32字节是一天的 有点奇怪 可能也是因为这个原因导致成交额的计算出了差错


--------------------


> **参考文章​:**
>  [读取股票软件的day文件数据][1]
>
>  [求大神C#编程：打开并读取格式为“.day”的文件！！！求助！！！][2]
>
>  [java读取钱龙股票软件下载的日线数据核心代码][3]

----------

> Written with [StackEdit](https://stackedit.io/).


[1]: http://wenku.baidu.com/view/6945fdcfdd3383c4bb4cd28c.html
[2]: http://zhidao.baidu.com/link?url=pN6XlhbjW8GXVlakd-0ApxL81nanP1Xu_AmZEPu-m1jnupXij37Hg04ar5Cyi-RpaJfSnnC16tl2YUTh0Ln2Sa&qq-pf-to=pcqq.c2c
[3]: http://www.voidcn.com/blog/luangj/article/p-4569729.html