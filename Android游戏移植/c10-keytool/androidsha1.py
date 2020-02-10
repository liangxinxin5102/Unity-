if __name__ == '__main__':
    import os

    # 为了能执行 keytool.exe 将JDK加入环境变量, 
    os.putenv('Path', 'C:/Program Files/Java/jdk1.8.0_91/bin/')

    # 将路径切换到.android
    os.chdir('c:/Users/DIY/.android')

    # 执行keytool命令
    command = 'keytool -list -v -keystore debug.keystore'
    r = os.popen(command) # 执行该命令
    info = r.readlines()  # 读取命令行的输出到一个list
    for line in info:  # 按行遍历 显示结果
        line = line.strip('\r\n')
        print(line)
    

