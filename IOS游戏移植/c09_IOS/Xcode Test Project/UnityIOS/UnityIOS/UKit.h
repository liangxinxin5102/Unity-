//
//  UKit.h
//  UnityIOS
//
//  Created by bushi on 17/3/30.
//  Copyright © 2017年 NT. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface UKit : NSObject
+(UKit*) instance;
// 显示一个IOS对话框
-(void) ShowWarningBox:(NSString*) strTitle : (NSString*) strText;
@end
