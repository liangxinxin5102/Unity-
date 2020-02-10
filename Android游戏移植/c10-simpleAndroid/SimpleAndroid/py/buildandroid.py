"""
权利说明
本脚本为《Unity3d/2d手机游戏开发（第三版)》的教学代码,
任何购买该书的用户均可以免费使用，但请保留本权利说明，谢谢!
"""
import os
import shutil


# 定义一个函数，从Unity中导出Android工程文件
def export_android(unity_ed_path, project_path, log_path):

    # 如果当前工程已经存在，删除这个工程
    if os.path.exists('./export project'):
        shutil.rmtree('./export project')

    # 将Unity可执行文件添加到环境变量路径
    os.putenv("path", unity_ed_path)

    # unity命令行
    command = 'Unity.exe -quit -batchmode -projectPath {0} -logFile {1}\
     -executeMethod GameBuilder.BuildForAndroid'.format(project_path, log_path)

    # 执行命行令
    os.system(command)

    # 备份文件位置
    source = './UnityPlayerActivity.java'
    # 目标位置
    target = './export project/SimpleAndroid/src/com/demo/simpleandroid/UnityPlayerActivity.java'
    # 执行复制
    shutil.copy(source, target)

    print('Android工程导出完成')


def build_android():
    print("start to build android")
    # 检查是否已经存在build.xml文件
    if os.path.exists('./build.xml'):
        os.remove('./build.xml')
    # 检查是否已经存在local.properties文件, 如果存在则删除
    if os.path.exists('./local.properties'):
        os.remove('./local.properties')

    # 数字签名备份文件位置
    source = '../../user.jks'
    # 目标位置
    target = './user.jks'
    # 执行复制
    shutil.copy(source, target)

    # 添加android和ant批处理程序的所在路径到环境变量, 添加C:/WINDOWS/system32/是为了确保能找到该目录下面的xcopy.exe
    os.putenv("Path", 'D:/Android/sdk/tools/;D:/apache-ant-1.9.7/bin/;C:/WINDOWS/system32/')
    # 添加android SDK到环境变量
    os.putenv("ANDROID_HOME", 'D:/Android/sdk/')

    # 更新android工程
    os.system('android update project --target android-23 --path ./ --name SimpleAndroid')

    # 写入数字签名信息
    with open('local.properties', 'a') as f:
        f.write('key.store=./user.jks\n')
        f.write('key.alias=unitydemoapp\n')
        f.write('key.store.password=123456\n')
        f.write('key.alias.password=123456\n')

    os.system('ant clean')
    # 创建apk
    os.system('ant release')
    print('Android工程导出完成')


if __name__ == '__main__':
    # 更改当前路径位置
    os.chdir('../../')

    # Unity编辑器安装路径
    unity_editor_path = 'C:/Program Files/Unity/Editor'
    # Unity工程路径
    unity_project_path = 'D:/Documents/cd/c10-simpleAndroid/SimpleAndroid'
    # Log存放位置
    build_log_path = './buildlog.txt'

    # 执行export_android函数
    export_android(unity_editor_path, unity_project_path, build_log_path)

    # 更新当前路径到输出的android工程路径
    os.chdir('./export project/SimpleAndroid')
    #build_android()
