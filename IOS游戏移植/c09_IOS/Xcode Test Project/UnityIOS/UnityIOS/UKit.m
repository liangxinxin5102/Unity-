//
//  UKit.m
//  UnityIOS
//
//  Created by bushi on 17/3/30.
//  Copyright © 2017年 NT. All rights reserved.
//
#import <UIKit/UIKit.h>
#import "UKit.h"

@implementation UKit

static UKit *_instance=nil;

+ (UKit*) instance
{
    if (_instance==nil)
    {
        _instance=[[UKit alloc]init];
    }
    
    return _instance;
}

-(void) ShowWarningBox:(NSString*) strTitle : (NSString*) strText
{
    // 创建UIAlertController
    UIAlertController *alertController = [UIAlertController alertControllerWithTitle:strTitle message:strText preferredStyle:UIAlertControllerStyleAlert];
    // 添加行为
    UIAlertAction *okAction = [UIAlertAction actionWithTitle:@"OK" style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action){
        NSLog(@"点击OK按钮"); // 回调
        NSString *msg = @"IOS Message";
        //向Unity发送消息，这里接收对象名字是Main Camera, 接收函数名是OnButtonClick，带一个字符串参数
        UnitySendMessage("Main Camera", "OnButtonClick", [msg UTF8String] );
        
    }];
    [alertController addAction:okAction];
    // 获取当前UIViewController
    //UIViewController *viewController = [[[UIApplication sharedApplication] keyWindow] rootViewController];
    // 使用Unity提供的方法获取当前UIViewController
    UIViewController *viewController = UnityGetGLViewController();
    // 显示对话框
    [viewController presentViewController:alertController animated:YES completion:nil];
    NSLog(@"显示对话框");
}
@end

// 将的Unity中引用的代码
extern void ShowWarningBox(char* strTitle ,char* strText)
{
    [[UKit instance] ShowWarningBox:[NSString stringWithUTF8String: strTitle]:[NSString stringWithUTF8String: strText]];
}


