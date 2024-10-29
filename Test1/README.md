# 题目
* 1)实现 move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong){
	}
		使gameObject在time秒内，从begin移动到end，若pingpong为true，则在结束时使gameObject在time秒内从end移动到begin，如此往复。
* 2)在上题基础上实现 easeIn easeOut easeInOut 动画效果

# 实现
### MoveAnim.cs
https://github.com/youZhuang/JoyCastleInterview/blob/main/Test1/MoveAnim.cs
* Move 完成基本的动画
* MoveWithEase 实现了Ease动画效果
